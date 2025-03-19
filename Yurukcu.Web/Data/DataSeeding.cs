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

            var addresses = new Address
            {
                AddressTitle = "Adres başlığı Billing false",
                IsBillingAddress = false,
                City = "Kartal",
                DeliveryAddress = "Teslimat adresi",
                ZipCode = "34",
                UserId = 2
            };

            var supportRequest = new SupportRequest
            {
                Name = "Sıtkı Dede",
                EMail = "sitkidede60@gmaiil.com",
                Message = "Mesajım",
                SubmittedDate = DateTime.Now
            };

            var shoppingBag = new ShoppingBag
            {
                Price = 15,           // Fiyat
                ProductId = 1,        // Ürün ID'si
                Quantity = 15,        // Adet
                UserId = 2,         // Kullanıcı ID'si
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
            if (!context.OrderDetails.Any())
            {
                context.OrderDetails.AddRange(orderDetails);
                context.SaveChanges();
            }
            if (!context.Addresses.Any())
            {
                context.Addresses.AddRange(addresses);
                context.SaveChanges();
            }
            if (!context.SupportRequests.Any())
            {
                context.SupportRequests.AddRange(supportRequest);
                context.SaveChanges();
            }
            if (!context.ShoppingBags.Any())
            {
                context.ShoppingBags.AddRange(shoppingBag);
                context.SaveChanges();
            }
        }
    }
}