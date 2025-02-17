﻿using Learn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Learn.DataAccess;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
   public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
   {

   }

   public DbSet<Category> Categories { get; set; }
   public DbSet<Product> Products { get; set; }
   public DbSet<Company> Companies { get; set; }
   public DbSet<ApplicationUser> ApplicationUsers { get; set; }
   public DbSet<ShoppingCart> ShoppingCarts { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Company>().HasData(
         new Company { Id = 1, Name = "Company 1", StreetAddress = "123 Main St", City = "Anytown", State = "CA", PostalCode = "12345", PhoneNumber="1234567879" },
         new Company { Id = 2, Name = "Company 2", StreetAddress = "456 Elm St", City = "Othertown", State = "NY", PostalCode = "67890", PhoneNumber = "1234567879" },
         new Company { Id = 3, Name = "Company 3", StreetAddress = "789 Oak St", City = "Sometown", State = "TX", PostalCode = "54321", PhoneNumber = "1234567879" }
      );
      modelBuilder.Entity<Category>().HasData(
         new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
         new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
         new Category { Id = 3, Name = "History", DisplayOrder = 3 }
      );

      modelBuilder.Entity<Product>().HasData(
         new Product { Id = 7, Title = "The Hobbit", ISBN = "123456789", Author = "J.R.R. Tolkien", Description = "A great book", ListPrice = 19.99, Price = 14.99, Price50 = 12.99, Price100 = 9.99, CategoryId = 1, ImageURL = "" },
         new Product { Id = 8, Title = "The Lord of the Rings", ISBN = "987654321", Author = "J.R.R. Tolkien", Description = "A great book", ListPrice = 29.99, Price = 24.99, Price50 = 22.99, Price100 = 19.99, CategoryId = 2, ImageURL = "" },
         new Product { Id = 9, Title = "The Silmarillion", ISBN = "123123123", Author = "J.R.R. Tolkien", Description = "A great book", ListPrice = 24.99, Price = 19.99, Price50 = 17.99, Price100 = 14.99, CategoryId = 3, ImageURL = "" },
         new Product
         {
            Id = 1,
            Title = "Fortune of Time",
            Author = "Billy Spark",
            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
            ISBN = "SWD9999001",
            ListPrice = 99,
            Price = 90,
            Price50 = 85,
            Price100 = 80,
            CategoryId = 1,
            ImageURL = ""
         },
         new Product
         {
            Id = 2,
            Title = "Dark Skies",
            Author = "Nancy Hoover",
            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
            ISBN = "CAW777777701",
            ListPrice = 40,
            Price = 30,
            Price50 = 25,
            Price100 = 20,
            CategoryId = 2,
            ImageURL = ""
         },
         new Product
         {
            Id = 3,
            Title = "Vanish in the Sunset",
            Author = "Julian Button",
            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
            ISBN = "RITO5555501",
            ListPrice = 55,
            Price = 50,
            Price50 = 40,
            Price100 = 35,
            CategoryId = 3,
            ImageURL = ""
         },
         new Product
         {
            Id = 4,
            Title = "Cotton Candy",
            Author = "Abby Muscles",
            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
            ISBN = "WS3333333301",
            ListPrice = 70,
            Price = 65,
            Price50 = 60,
            Price100 = 55,
            CategoryId = 1,
            ImageURL = ""
         },
         new Product
         {
            Id = 5,
            Title = "Rock in the Ocean",
            Author = "Ron Parker",
            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
            ISBN = "SOTJ1111111101",
            ListPrice = 30,
            Price = 27,
            Price50 = 25,
            Price100 = 20,
            CategoryId = 2,
            ImageURL = ""
         },
         new Product
         {
            Id = 6,
            Title = "Leaves and Wonders",
            Author = "Laura Phantom",
            Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
            ISBN = "FOT000000001",
            ListPrice = 25,
            Price = 23,
            Price50 = 22,
            Price100 = 20,
            CategoryId = 3,
            ImageURL = ""
         }
      );
   }
}
