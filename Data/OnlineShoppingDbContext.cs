using OnlineShopping.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OnlineShopping.Data
{

    public class OnlineShoppingDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public OnlineShoppingDbContext(DbContextOptions<OnlineShoppingDbContext> options) : base(options)
        {
        }
    }
}


