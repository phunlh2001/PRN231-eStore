using BusinessObjects.Objects;
using Microsoft.EntityFrameworkCore;

namespace BusinessObjects.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                    new Category { CategoryId = 1, CategoryName = "Apple" },
                    new Category { CategoryId = 2, CategoryName = "Samsung" },
                    new Category { CategoryId = 3, CategoryName = "Nokia" }
                );

            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    MemberId = 1,
                    Email = "admin@estore.com",
                    Password = "admin@@",
                    CompanyName = "FPT",
                    City = "Can Tho",
                    Country = "Vietnam"
                },

                new Member
                {
                    MemberId = 2,
                    Email = "phunlh2001@gmail.com",
                    Password = "123456",
                    CompanyName = "Lifecom",
                    City = "Dong Thap",
                    Country = "Vietnam"
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    ProductName = "Samsung Galaxy",
                    CategoryId = 2,
                    UnitPrice = 200,
                    UnitslnStock = 100,
                    Weight = "50"
                },

                new Product
                {
                    ProductId = 2,
                    ProductName = "Nokia 1280",
                    CategoryId = 3,
                    UnitPrice = 90,
                    UnitslnStock = 100,
                    Weight = "90"
                },

                new Product
                {
                    ProductId = 3,
                    ProductName = "iPhone 16",
                    CategoryId = 1,
                    UnitPrice = 1200,
                    UnitslnStock = 100,
                    Weight = "40"
                }
            );
        }

        public static void SetUp(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>().HasKey(o => new
            {
                o.ProductId,
                o.OrderId
            });
        }
    }
}
