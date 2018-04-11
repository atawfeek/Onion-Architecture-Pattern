using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MLS.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLS.Infrastructure.Data
{
    public static class DbInitializer
    {
        private static readonly string[] Roles = new string[] { "Administrator", "Supplier", "Business", "Publisher" };

        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    await dbContext.Database.MigrateAsync();

                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    foreach (var role in Roles)
                    {
                        if (!await roleManager.RoleExistsAsync(role))
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                        }
                    }
                }
            }
        }

        public static async Task Initialize(ApplicationDbContext context, IServiceProvider serviceProvider) //SchoolContext is EF context
        {
            context.Database.EnsureCreated();//if db is not exist ,it will create database .but ,do nothing .


            // Look for any students.
            if (!context.Accounts.Any())
            {
                List<Account> _account = new List<Account>
                {
                    new Account()
                    {
                        AccountName = "Baccah",
                        CreatedDate = DateTime.Now,
                        Email = "info@baccah.com",
                    }
                };

                context.Accounts.AddRange(_account);
                context.SaveChanges();

                //Provision User
                var adminID = await EnsureUser(serviceProvider, "P@ssw0rd!", "admin@baccah.com", _account[0].Id, context);
                await EnsureRole(serviceProvider, adminID, "Administrator");
            }


            // Look for any students.
            if (!context.Books.Any())
            {
                Book book1 = new Book()
                {
                    CreatedDate = DateTime.Now,
                    Description = "Welcome to ASP.NET Core! ASP.NET Core is a lean and composable framework for building web and cloud applications. ASP.NET Core is fully open source and available on GitHub. ASP.NET Core is available on Windows, Mac, and Linux.",
                    Edition = "1st",
                    ExternalId = "644e1dd7-2a7f-18fb-b8ed-ed78c3f92c2b",
                    ISBN13 = "9783161484100",
                    NumberOfPages = 23,
                    Price = 300,
                    PublicationDate = DateTime.Now.ToString(),
                    Publisher = "Nahdet Misr",
                    Title = ".Net Core",
                    Subtitle = "The Little ASP.NET Core Book",
                    TableOfContents = "With the series coming to an end, it's time to look back at these last 22 days of content, and see how they relate to the outline of my upcoming book \"Front - end Development with ASP.NET Core, Angular, and Bootstrap.\"",
                    HasImage = true
                };
                Author author1 = new Author()
                {
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Name = "Matt Frisbie"
                };
                Author author11 = new Author()
                {
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Name = "Matt Frisbie (2)"
                };
                BookAuthor bookAuthor1 = new BookAuthor()
                {
                    Book = book1,
                    Author = author1
                };
                BookAuthor bookAuthor11 = new BookAuthor()
                {
                    Book = book1,
                    Author = author11
                };
                Category category1 = new Category()
                {
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Name = "Technology"
                };
                Category category11 = new Category()
                {
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Name = "Technology (2)"
                };
                BookCategory bookCategory1 = new BookCategory()
                {
                    Book = book1,
                    Category = category1
                };
                BookCategory bookCategory11 = new BookCategory()
                {
                    Book = book1,
                    Category = category11
                };

                /// second book
                Book book2 = new Book()
                {
                    CreatedDate = DateTime.Now,
                    Description = "Welcome to Game development.",
                    Edition = "2nd",
                    ExternalId = "32B17041-CE53-4A14-9D3C-680DBE63B85A",
                    ISBN13 = "5683171484111",
                    NumberOfPages = 23,
                    Price = 450,
                    PublicationDate = DateTime.Now.ToString(),
                    Publisher = "Nahdet Misr",
                    Title = "Game Development",
                    Subtitle = "Game Development",
                    TableOfContents = "gaming content",
                    HasImage = true
                };
                Author author2 = new Author()
                {
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Name = "John Fohler"
                };
                BookAuthor bookAuthor2 = new BookAuthor()
                {
                    Book = book2,
                    Author = author2
                };
                Category category2 = new Category()
                {
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Name = "Games"
                };
                BookCategory bookCategory2 = new BookCategory()
                {
                    Book = book2,
                    Category = category2
                };

                /// 3rd book
                Book book3 = new Book()
                {
                    CreatedDate = DateTime.Now,
                    Description = "You DONT Know JS",
                    Edition = "1st",
                    ExternalId = "4FC19599-574C-413F-8E3F-835D523B633D",
                    ISBN13 = "9783675484199",
                    NumberOfPages = 23,
                    Price = 300,
                    PublicationDate = DateTime.Now.ToString(),
                    Publisher = "Nahdet Misr",
                    Title = "JavaScript",
                    Subtitle = "Learn JS",
                    TableOfContents = "With the series coming to an end, time to learn JavaScript.",
                    HasImage = true
                };
                Author author3 = new Author()
                {
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Name = "Mohamed El-Pasha"
                };
                Author author33 = new Author()
                {
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Name = "Mohamed El-Pasha (2)"
                };
                BookAuthor bookAuthor3 = new BookAuthor()
                {
                    Book = book3,
                    Author = author3
                };
                BookAuthor bookAuthor33 = new BookAuthor()
                {
                    Book = book3,
                    Author = author33
                };
                Category category3 = new Category()
                {
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Name = "FrontEnd"
                };
                BookCategory bookCategory3 = new BookCategory()
                {
                    Book = book3,
                    Category = category3
                };

                /// 4th book
                Book book4 = new Book()
                {
                    CreatedDate = DateTime.Now,
                    Description = "Xamarin!",
                    Edition = "1st",
                    ExternalId = "94D67ACF-4DC0-474F-A77E-FB68CD7E4BE7",
                    ISBN13 = "9783161484122",
                    NumberOfPages = 23,
                    Price = 700,
                    PublicationDate = DateTime.Now.ToString(),
                    Publisher = "App Publisher",
                    Title = "Cross Platform",
                    Subtitle = "Cross Book",
                    TableOfContents = "content of cross platform series.",
                    HasImage = true
                };
                Author author4 = new Author()
                {
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Name = "Karim Mobile"
                };
                BookAuthor bookAuthor4 = new BookAuthor()
                {
                    Book = book4,
                    Author = author4
                };
                Category category4 = new Category()
                {
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Name = "Cross Platform"
                };
                Category category44 = new Category()
                {
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Name = "Cross Platform (2)"
                };
                BookCategory bookCategory4 = new BookCategory()
                {
                    Book = book3,
                    Category = category4
                };
                BookCategory bookCategory44 = new BookCategory()
                {
                    Book = book3,
                    Category = category44
                };

                context.Books.Add(book1);
                context.Authors.Add(author1);
                context.Authors.Add(author11);
                context.BookAuthors.Add(bookAuthor1);
                context.BookAuthors.Add(bookAuthor11);
                context.Categories.Add(category1);
                context.Categories.Add(category11);
                context.BookCategories.Add(bookCategory1);
                context.BookCategories.Add(bookCategory11);

                context.Books.Add(book2);
                context.Authors.Add(author2);
                context.BookAuthors.Add(bookAuthor2);
                context.Categories.Add(category2);
                context.BookCategories.Add(bookCategory2);

                context.Books.Add(book3);
                context.Authors.Add(author3);
                context.Authors.Add(author33);
                context.BookAuthors.Add(bookAuthor3);
                context.BookAuthors.Add(bookAuthor33);
                context.Categories.Add(category3);
                context.BookCategories.Add(bookCategory3);

                context.Books.Add(book4);
                context.Authors.Add(author4);
                context.BookAuthors.Add(bookAuthor4);
                context.Categories.Add(category4);
                context.Categories.Add(category44);
                context.BookCategories.Add(bookCategory4);
                context.BookCategories.Add(bookCategory44);

                context.SaveChanges();
            }
            
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName, 
                                                    int accountId, ApplicationDbContext context)
        {
            var userManager = serviceProvider.GetService<UserManager<AppUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new AppUser { UserName = UserName };
                await userManager.CreateAsync(user, testUserPw);

                User admin = new User()
                {
                    AccountId = accountId,
                    CreatedDate = DateTime.Now,
                    Identity = user
                };

                context.AppUsers.Add(admin);
                context.SaveChanges();
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<AppUser>>();

            var user = await userManager.FindByIdAsync(uid);

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

    }
}
