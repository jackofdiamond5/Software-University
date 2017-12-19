using System.IO;
using Newtonsoft.Json;
using ProductsShop.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductsShop
{
    public class ShopContext : DbContext
    {
        public ShopContext() { }

        public ShopContext(DbContextOptions options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryProduct> CategoryProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            var optionsString = File.ReadAllText("Connection.json");
            dynamic optionsParsed = JsonConvert.DeserializeObject(optionsString);
            string parsedString = optionsParsed.ConnectionString.ToString();

            optionsBuilder.UseSqlServer(parsedString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                .HasMaxLength(40)
                .IsRequired(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(40)
                    .IsRequired(false);

                entity.Property(e => e.Age)
                .IsRequired(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.HasOne(e => e.Buyer)
                .WithMany(b => b.BoughtProducts)
                .HasForeignKey(e => e.BuyerId);

                entity.HasOne(e => e.Seller)
                    .WithMany(b => b.SoldProducts)
                    .HasForeignKey(e => e.SellerId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
            });

            modelBuilder.Entity<CategoryProduct>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.ProductId });

                entity.HasOne(e => e.Product)
                .WithMany(p => p.Categories)
                .HasForeignKey(e => e.ProductId);

                entity.HasOne(e => e.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(e => e.CategoryId);
            });
        }
    }
}
