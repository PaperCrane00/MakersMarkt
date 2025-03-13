using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkersMarktApp.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Role_User> Role_Users { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Feature_Specific> Feature_Specifics { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_Review> Product_Reviews { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Seller_Review> Sales_Reviews { get; set; }
        public DbSet<Specific> Specifics { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Material_Specific> Material_Specifics { get; set; }

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
            modelBuilder.Entity<Seller_Review>()
                .HasOne(sr => sr.Seller)
                .WithMany(u => u.Seller_Reviews)
                .HasForeignKey(sr => sr.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Seller_Review>()
                .HasOne(sr => sr.Reviewer)
                .WithMany()
                .HasForeignKey(sr => sr.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
