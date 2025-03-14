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
            ProductName = "Product 1 Name",
            Price = 123,
            ProductDescription="Product 1 Description",
            ImageUrl="products/product1.jpg"
        },
        new Product
        {
            ProductName = "Product 2 Name",
            Price = 456,
            ProductDescription="Product 2 Description",
            ImageUrl="products/product2.jpg"
        },
        new Product
        {
            ProductName = "Product 3 Name",
            Price = 789,
            ProductDescription="Product 3 Description",
            ImageUrl="products/product3.jpg"
        },
        new Product
        {
            ProductName = "Product 1 Name",
            Price = 123,
            ProductDescription="Product 1 Description",
            ImageUrl="products/product1.jpg"
        },
        new Product
        {
            ProductName = "Product 2 Name",
            Price = 456,
            ProductDescription="Product 2 Description",
            ImageUrl="products/product2.jpg"
        },
        new Product
        {
            ProductName = "Product 3 Name",
            Price = 789,
            ProductDescription="Product 3 Description",
            ImageUrl="products/product3.jpg"
        },new Product
        {
            ProductName = "Product 1 Name",
            Price = 123,
            ProductDescription="Product 1 Description",
            ImageUrl="products/product1.jpg"
        },
        new Product
        {
            ProductName = "Product 2 Name",
            Price = 456,
            ProductDescription="Product 2 Description",
            ImageUrl="products/product2.jpg"
        },
        new Product
        {
            ProductName = "Product 3 Name",
            Price = 789,
            ProductDescription="Product 3 Description",
            ImageUrl="products/product3.jpg"
        },new Product
        {
            ProductName = "Product 1 Name",
            Price = 123,
            ProductDescription="Product 1 Description",
            ImageUrl="products/product1.jpg"
        },
        new Product
        {
            ProductName = "Product 2 Name",
            Price = 456,
            ProductDescription="Product 2 Description",
            ImageUrl="products/product2.jpg"
        },
        new Product
        {
            ProductName = "Product 3 Name",
            Price = 789,
            ProductDescription="Product 3 Description",
            ImageUrl="products/product3.jpg"
        },
    };
            var discountProduct = new List<DiscountProduct>()
    {
        new DiscountProduct
        {
            ProductName = "Product 1 Name",
            LowPrice = 123,
            HighPrice = 456,
            ProductDescription="Product 1 Description",
            ImageUrl="products/product1.jpg"
        },
        new DiscountProduct
        {
            ProductName = "Product 2 Name",
            LowPrice = 456,
            HighPrice = 789,
            ProductDescription="Product 2 Description",
            ImageUrl="products/product2.jpg"
        },
        new DiscountProduct
        {
            ProductName = "Product 3 Name",
            LowPrice = 123,
            HighPrice = 789,
            ProductDescription="Product 3 Description",
            ImageUrl="products/product3.jpg"
        },
        new DiscountProduct
        {
            ProductName = "Product 1 Name",
            LowPrice = 123,
            HighPrice = 456,
            ProductDescription="Product 1 Description",
            ImageUrl="products/product1.jpg"
        },
        new DiscountProduct
        {
            ProductName = "Product 1 Name",
            LowPrice = 123,
            HighPrice = 456,
            ProductDescription="Product 1 Description",
            ImageUrl="products/product1.jpg"
        },
        new DiscountProduct
        {
            ProductName = "Product 1 Name",
            LowPrice = 123,
            HighPrice = 456,
            ProductDescription="Product 1 Description",
            ImageUrl="products/product1.jpg"
        },
        new DiscountProduct
        {
            ProductName = "Product 1 Name",
            LowPrice = 123,
            HighPrice = 456,
            ProductDescription="Product 1 Description",
            ImageUrl="products/product1.jpg"
        },
        new DiscountProduct
        {
            ProductName = "Product 1 Name",
            LowPrice = 123,
            HighPrice = 456,
            ProductDescription="Product 1 Description",
            ImageUrl="products/product1.jpg"
        },
         new DiscountProduct
        {
            ProductName = "Product 1 Name",
            LowPrice = 123,
            HighPrice = 456,
            ProductDescription="Product 1 Description",
            ImageUrl="products/product1.jpg"
        },
        new DiscountProduct
        {
            ProductName = "Product 1 Name",
            LowPrice = 123,
            HighPrice = 456,
            ProductDescription="Product 1 Description",
            ImageUrl="products/product1.jpg"
        },
        new DiscountProduct
        {
            ProductName = "Product 1 Name",
            LowPrice = 123,
            HighPrice = 456,
            ProductDescription="Product 1 Description",
            ImageUrl="products/product1.jpg"
        },
        new DiscountProduct
        {
            ProductName = "Product 1 Name",
            LowPrice = 123,
            HighPrice = 456,
            ProductDescription="Product 1 Description",
            ImageUrl="products/product1.jpg"
        },
    };
            var users = new List<User>()
            {
                new User{
                    FullName="Sıtkı Dede",
                    EMail="sitkicandede@gmail.com",
                    PhoneNumber="05078871701",
                    Address="Address 1",
                    Birthday = DateTime.Parse("2000-08-06"),
                    CreatedDate = DateTime.Now,
                    Gender="Erkek",
                    Password="123456"
                }
            };

            if (!context.DiscountProducts.Any())
            {
                context.DiscountProducts.AddRange(discountProduct);
                context.SaveChanges();
            }
            if (!context.Products.Any())
            {
                context.Products.AddRange(product);
                context.SaveChanges();
            }
            if (!context.Users.Any())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}