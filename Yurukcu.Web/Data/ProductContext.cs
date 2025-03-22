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
        public DbSet<User> Users { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<SupportRequest> SupportRequests { get; set; }
        public DbSet<ShoppingBag> ShoppingBags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(10,2)");
            
            modelBuilder.Entity<Product>()
                .Property(p => p.WithoutDiscountPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderDetail>()
                .Property(p => p.TotalPrice)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<ShoppingBag>()
                .HasOne(s => s.User)
                .WithMany(u => u.ShoppingBags)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<ShoppingBag>()
                .Property(p => p.Price)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<OrderDetail>()
                .HasKey(o => o.OrderId);
        }
    }
}