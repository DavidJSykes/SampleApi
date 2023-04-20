using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using VpDataAccess.Models;

namespace VpDataAccess.Data
{
    public static class DataSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<VpDbContext>();

                if (!context.Customers.Any())
                {
                    var customers = new List<Customer>
                    {
                        new Customer
                        {
                            FirstName = "David",
                            LastName = "Sykes"
                        },
                        new Customer
                        {
                            FirstName = "Jane",
                            LastName = "Doe"
                        },
                        new Customer
                        {
                            FirstName = "Bob",
                            LastName = "Smith"
                        }
                    };
                    context.Customers.AddRange(customers);
                }

                if (!context.Products.Any())
                {
                    var products = new List<Product>()
                    {
                        new Product
                        {
                            Name = "Test Product 1",
                            Price = 5.0M
                        },
                        new Product
                        {
                            Name = "Test Product 2",
                            Price = 6.25M
                        },
                        new Product
                        {
                            Name = "Test Product 3",
                            Price = 18.22M
                        }
                    };
                    context.Products.AddRange(products);
                }

                if (!context.Orders.Any())
                {
                    var orders = new List<Order>()
                    {
                        new Order
                        {
                            CreatedAt = DateTime.Now,
                            CustomerID = 1,
                            OrderItems = new List<OrderItem>()
                            {
                                new OrderItem
                                {
                                    ProductId = 1,
                                    Quantity = 2,
                                    Price = 10M
                                }
                            }
                        }
                    };
                    context.Orders.AddRange(orders);
                }
                context.SaveChanges();
            }
        }
    }
}
