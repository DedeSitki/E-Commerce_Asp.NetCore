using Microsoft.EntityFrameworkCore;
using Yurukcu.Web.Entity;

namespace Yurukcu.Web.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<DiscountProduct> DiscountProducts { get; set; }
        public DbSet<User> Users{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<DiscountProduct>()
                .Property(p => p.HighPrice)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<DiscountProduct>()
                .Property(p => p.LowPrice)
                .HasColumnType("decimal(10,2)");
        }
    }
}