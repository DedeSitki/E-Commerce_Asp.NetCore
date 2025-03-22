using Microsoft.EntityFrameworkCore;
using Yurukcu.Web.Entity;

namespace Yurukcu.Web.Data
{
    public class DataSeeding
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProductContext>();

            context.Database.Migrate();

            var product = new List<Product>()
            {
                new Product
                {
                    ProductName = "Telefon",
                    ProductDescription = "Akıllı telefon",
                    ImageUrl="products/product1.jpg",
                    IsDiscounted = true,
                    Price = 4500,
                    WithoutDiscountPrice = 5000
                },
                new Product
                {
                    ProductName = "Kulaklık",
                    ProductDescription = "Bluetooth kulaklık",
                    Price = 1000,
                    ImageUrl="products/product2.jpg",
                    IsDiscounted = false
                },
                 new Product
                {
                    ProductName = "Kulaklık",
                    ProductDescription = "Bluetooth kulaklık",
                    Price = 1000,
                    ImageUrl="products/product3.jpg",
                    IsDiscounted = false
                },
                new Product
                {
                    ProductName = "Telefon",
                    ProductDescription = "Akıllı telefon",
                    ImageUrl="products/product1.jpg",
                    IsDiscounted = true,
                    Price = 4500,
                    WithoutDiscountPrice = 5000
                },
                new Product
                {
                    ProductName = "Kulaklık",
                    ProductDescription = "Bluetooth kulaklık",
                    Price = 1000,
                    ImageUrl="products/product2.jpg",
                    IsDiscounted = false
                },
                 new Product
                {
                    ProductName = "Kulaklık",
                    ProductDescription = "Bluetooth kulaklık",
                    Price = 1000,
                    ImageUrl="products/product3.jpg",
                    IsDiscounted = false
                },
                new Product
                {
                    ProductName = "Telefon",
                    ProductDescription = "Akıllı telefon",
                    ImageUrl="products/product1.jpg",
                    IsDiscounted = true,
                    Price = 4500,
                    WithoutDiscountPrice = 5000
                },
                new Product
                {
                    ProductName = "Kulaklık",
                    ProductDescription = "Bluetooth kulaklık",
                    Price = 1000,
                    ImageUrl="products/product2.jpg",
                    IsDiscounted = false
                },
                 new Product
                {
                    ProductName = "Kulaklık",
                    ProductDescription = "Bluetooth kulaklık",
                    Price = 1000,
                    ImageUrl="products/product3.jpg",
                    IsDiscounted = false
                },
            };

            var orderDetails = new List<OrderDetail>()
            {
                new OrderDetail
                {
                    UserId = 2,
                    OrderDate = DateTime.Now,
                    OrderAddress = "Kartal",
                    OrderBillingAddress = "Soğanlık",
                    Orders = ["Ürün 1", "Ürün 2"],
                    TotalPrice = 1050,
                    OrderStatus ="Ödendi",
                    PaymentMethod="Kredi Kartı",
                    OrderTrackingCode = "123A456BC"
                }
            };

            if (!context.Products.Any())
            {
                context.Products.AddRange(product);
                context.SaveChanges();
            }
            if (!context.OrderDetails.Any())
            {
                context.OrderDetails.AddRange(orderDetails);
                context.SaveChanges();
            }
        }
    }
}