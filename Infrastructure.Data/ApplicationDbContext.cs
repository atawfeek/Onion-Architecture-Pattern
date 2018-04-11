using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MLS.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLS.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options)
              : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BookAuthor>().HasKey(e => new { e.BookId, e.AuthorId });
            builder.Entity<BookCategory>().HasKey(e => new { e.BookId, e.CategoryId });

            builder.Entity<BookCategory>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.Categories)
                .HasForeignKey(bc => bc.BookId);

            builder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(bc => bc.CategoryId);

            base.OnModelCreating(builder);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> AppUsers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
    }
}
