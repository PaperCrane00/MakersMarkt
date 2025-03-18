using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakersMarktApp.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<FeatureSpecific> FeatureSpecifics { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SellerReview> SalesReviews { get; set; }
        public DbSet<Specific> Specifics { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<MaterialSpecific> MaterialSpecifics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                ConfigurationManager.ConnectionStrings["MakersMarkt-Database"].ConnectionString,
                ServerVersion.Parse("8.0.30")
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseCollation("utf8mb4_0900_as_cs");
            var feature = new Faker<Feature>()
                .RuleFor(i => i.Id, f => f.IndexFaker + 1)
                .RuleFor(i => i.Description, f => f.Lorem.Paragraph())
                .Generate(100);
            modelBuilder.Entity<Feature>().HasData(feature);
            var featureSpecific = new Faker<FeatureSpecific>()
                .RuleFor(i => i.Id, f => f.IndexFaker + 1)
                .RuleFor(i => i.FeatureId, f => f.Random.Int(1, 100))
                .RuleFor(i => i.SpecificId, f => f.Random.Int(1, 100))
                .Generate(100);
            modelBuilder.Entity<FeatureSpecific>().HasData(featureSpecific);
            var material = new Faker<Material>()
                .RuleFor(i => i.Id, f => f.IndexFaker + 1)
                .RuleFor(i => i.Title, f => f.Commerce.Product())
                .Generate(100);
            modelBuilder.Entity<Material>().HasData(material);
            var materialSpecific = new Faker<MaterialSpecific>()
                .RuleFor(i => i.Id, f => f.IndexFaker + 1)
                .RuleFor(i => i.MaterialId, f => f.Random.Int(1, 100))
                .RuleFor(i => i.SpecificId, f => f.Random.Int(1, 100))
                .Generate(100);
            modelBuilder.Entity<MaterialSpecific>().HasData(materialSpecific);
            var notification = new Faker<Notification>()
                .RuleFor(i => i.Id, f => f.IndexFaker + 1)
                .RuleFor(i => i.Title, f => f.Lorem.Words(2).ToString())
                .RuleFor(i => i.UserId, f => f.Random.Int(1, 100))
                .RuleFor(i => i.Description, f => f.Lorem.Paragraph())
                .RuleFor(i => i.CreatedAt, f => f.Date.Between(DateTime.Now.AddDays(-30), DateTime.Now))
                .Generate(100);
            modelBuilder.Entity<Notification>().HasData(notification);
            var product = new Faker<Product>()
                .RuleFor(i => i.Id, f => f.IndexFaker + 1)
                .RuleFor(i => i.Name, f => f.Commerce.Product())
                .RuleFor(i => i.Description, f => f.Commerce.ProductDescription())
                .RuleFor(i => i.ImageLink, f => f.Image.PicsumUrl())
                .RuleFor(i => i.UserId, f => f.Random.Int(1, 5))
                .RuleFor(i => i.TypeId, f => f.Random.Int(1, 5))
                .RuleFor(i => i.SpecificId, f => f.Random.Int(1, 100))
                .RuleFor(i => i.Price, f => f.Random.Int(1, 1000))
                .RuleFor(i => i.IsVerified, f => f.Random.Bool())
                .RuleFor(i => i.Moderation, f => f.Random.Bool())
                .Generate(100);
            modelBuilder.Entity<Product>().HasData(product);
            var productReview = new Faker<ProductReview>()
                .RuleFor(i => i.Id, f => f.IndexFaker + 1)
                .RuleFor(i => i.ProductId, f => f.Random.Int(1, 100))
                .RuleFor(i => i.ReviewerId, f => f.Random.Int(1, 100))
                .RuleFor(i => i.Title, f => f.Commerce.ProductName())
                .RuleFor(i => i.Description, f => f.Commerce.ProductDescription())
                .RuleFor(i => i.Rating, f => f.Random.Int(0, 5))
                .RuleFor(i => i.Moderation, f => f.Random.Bool())
                .Generate(100);
            modelBuilder.Entity<ProductReview>().HasData(productReview);
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Title = "Maker"
                },
                new Role
                {
                    Id = 2,
                    Title = "Buyer"
                },
                new Role
                {
                    Id = 3,
                    Title = "Moderator"
                }
            );
            var roleUser = new Faker<RoleUser>()
                .RuleFor(i => i.Id, f => f.IndexFaker + 1)
                .RuleFor(i => i.UserId, f => f.IndexFaker + 1)
                .RuleFor(i => i.RoleId, f => f.Random.Int(1, 3))
                .Generate(100);
            modelBuilder.Entity<RoleUser>().HasData(roleUser);
            var sale = new Faker<Sale>()
                .RuleFor(i => i.Id, f => f.IndexFaker + 1)
                .RuleFor(i => i.ProductId, f => f.Random.Int(1, 100))
                .RuleFor(i => i.BuyerId, f => f.Random.Int(1, 100))
                .RuleFor(i => i.Status, f => f.Commerce.ProductAdjective())
                .RuleFor(i => i.StatusDescription, f => f.Commerce.ProductDescription())
                .RuleFor(i => i.DateSold, f => f.Date.Between(DateTime.Now.AddDays(-30), DateTime.Now))
                .Generate(100);
            modelBuilder.Entity<Sale>().HasData(sale);
            var sellerReview = new Faker<SellerReview>()
                .RuleFor(i => i.Id, f => f.IndexFaker + 1)
                .RuleFor(i => i.SellerId, f => f.Random.Int(1, 100))
                .RuleFor(i => i.ReviewerId, f => f.Random.Int(1, 100))
                .RuleFor(i => i.Title, f => f.Company.CompanyName())
                .RuleFor(i => i.Description, f => f.Commerce.ProductDescription())
                .RuleFor(i => i.Rating, f => f.Random.Int(0, 5))
                .RuleFor(i => i.Moderation, f => f.Random.Bool())
                .Generate(100);
            modelBuilder.Entity<SellerReview>().HasData(sellerReview);
            var specific = new Faker<Specific>()
                .RuleFor(i => i.Id, f => f.IndexFaker + 1)
                .RuleFor(i => i.ProdTime, f => f.Date.BetweenTimeOnly(TimeOnly.MinValue, TimeOnly.MaxValue))
                .RuleFor(i => i.Complexity, f => f.Lorem.Text())
                .RuleFor(i => i.Sustainability, f => f.Lorem.Text())
                .Generate(100);
            modelBuilder.Entity<Specific>().HasData(specific);
            var type = new Faker<Type>()
                .RuleFor(i => i.Id, f => f.IndexFaker + 1)
                .RuleFor(i => i.Title, f => f.Commerce.ProductName())
                .Generate(5);
            modelBuilder.Entity<Type>().HasData(type);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "TestUser",
                    Email = "Test@Mail.com",
                    Password = "TestPassword",
                    Credit = 99999,
                    Is_Verified = true
                }
            );
            var user = new Faker<User>()
                .RuleFor(i => i.Id, f => f.IndexFaker + 2)
                .RuleFor(i => i.Username, f => f.Internet.UserName())
                .RuleFor(i => i.Email, f => f.Internet.Email())
                .RuleFor(i => i.Password, f => f.Internet.Password())
                .RuleFor(i => i.Credit, f => f.Random.Int(0, 1000))
                .RuleFor(i => i.Is_Verified, f => f.Random.Bool())
                .Generate(99);
            modelBuilder.Entity<User>().HasData(user);
        }
    }
}
