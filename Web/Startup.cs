using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MLS.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MLS.Services;
using MLS.Services.Interfaces;
using MLS.Infrastructure.Data.Interfaces;
using AutoMapper;
using MLS.Domain;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MLS.Web.Auth;
using MLS.Web.Helpers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using NSwag.AspNetCore;
using NJsonSchema;
using System.Reflection;

namespace MLS.Web
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public static IConfiguration Configuration { get; private set; }
        private IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services to DI.
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddIdentity<MLS.Infrastructure.Data.Entities.AppUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            var skipHTTPS = Configuration.GetValue<bool>("LocalTest:skipHTTPS");
            // requires using Microsoft.AspNetCore.Mvc;
            services.Configure<MvcOptions>(options =>
            {
                // Set LocalTest:skipHTTPS to true to skip SSL requrement in 
                // debug mode. This is useful when not using Visual Studio.
                if (Environment.IsDevelopment() && !skipHTTPS)
                {
                    options.Filters.Add(new RequireHttpsAttribute());
                }
            });

            services.AddMvcCore()
            .AddAuthorization() // Note - this is on the IMvcBuilder, not the service collection
            .AddJsonFormatters(options => options.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.AddMvc();

            services.AddRouting();

            //fix circular problem
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<MLS.Infrastructure.Data.Entities.User, User>()
                .ForMember(dest => dest.Identity, opt => opt.Ignore())
                .PreserveReferences()
                .AfterMap((m, u) => u.Account.Users = null);
                cfg.CreateMap<MLS.Infrastructure.Data.Entities.Account, Account>()
                .PreserveReferences();

                cfg.CreateMap<Book, MLS.Infrastructure.Data.Entities.Book>()
                .ForMember(dest => dest.Authors, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .PreserveReferences()
                .AfterMap((src, dest) =>
                 {
                     dest.Authors = new List<MLS.Infrastructure.Data.Entities.BookAuthor>();
                     foreach (var author in src.Authors)
                     {
                        dest.Authors.Add(new MLS.Infrastructure.Data.Entities.BookAuthor()
                        {
                            BookId = src.Id,
                            Book = dest,
                            AuthorId = author.Id,
                            Author = new Infrastructure.Data.Entities.Author()
                            {
                                Id = author.Id,
                                Name = author.Name,
                                CreatedDate = author.CreatedDate,
                                ModifiedDate = author.ModifiedDate
                            }
                         });
                     }
                 })
                .PreserveReferences();

                cfg.CreateMap<MLS.Infrastructure.Data.Entities.Book, Book>()
                .ForMember(dest => dest.Authors, opt => opt.Ignore())//.MapFrom(p => p.Authors))
                .ForMember(dest => dest.Categories, opt => opt.Ignore())//.MapFrom(p => p.Categories))
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .PreserveReferences()
                .AfterMap((src, dest) =>
                {
                    if (src.HasImage)
                        dest.ImageUrl = "https://source.unsplash.com/1600x900/?book";
                })
                .PreserveReferences()
                .AfterMap((src, dest) =>
                {
                    dest.Authors = new List<Author>();
                    foreach (var bookAuthor in src.Authors)
                    {
                        dest.Authors.Add(new Author()
                        {
                            Id = bookAuthor.Author.Id,
                            Name = bookAuthor.Author.Name
                        });
                    }
                })
                .PreserveReferences()
                .AfterMap((src, dest) =>
                {
                    dest.Categories = new List<Category>();
                    foreach (var bookCategory in src.Categories)
                    {
                        dest.Categories.Add(new Category()
                        {
                            Id = bookCategory.Category.Id,
                            Name = bookCategory.Category.Name
                        });
                    }
                })
                .PreserveReferences();
            });

            services.AddCors(o => o.AddPolicy("ebookPolicy", builderPolicy =>
            {
                builderPolicy.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));


            //Register Service Layer in DI.
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();

            //Register Repository Layer in DI.
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddSingleton<IJwtFactory, JwtFactory>();

            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            // jwt wire up
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            string SecretKey = Configuration["SecretKey"];
            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });

            // add identity
            var builder = services.AddIdentityCore<MLS.Infrastructure.Data.Entities.AppUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app,
                                IHostingEnvironment env,
                                ApplicationDbContext appContext,
                                IServiceProvider serviceProvider,
                                ILoggerFactory loggerFactory)
        {
            ConfigureOpenAPI(app);

            app.UseCors("ebookPolicy");

            app.UseAuthentication();

            app.UseMvc();

            DbInitializer.SeedRoles(serviceProvider).Wait();
            DbInitializer.Initialize(appContext, serviceProvider).Wait();

            loggerFactory.AddFile("Logs/MLS-{Date}.txt");
            env.EnvironmentName = EnvironmentName.Production;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
        }

        private void ConfigureOpenAPI(IApplicationBuilder app)
        {
            //if (!Environment.IsDevelopment())
              //  return;

            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;
                settings.GeneratorSettings.DefaultEnumHandling = EnumHandling.String;
                settings.GeneratorSettings.Title = "MLS APIs";
            });
        }
    }
}
