using System.Collections.Generic;
using Ecommerce.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.OrderAPI.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<Order> tb_Order { get; set; }
        public DbSet<OrderDetail> tb_OrderDetail { get; set; }
        public DbSet<OrderStatus> tb_OrderStatus { get; set; }
        public DbSet<Product> tb_Product { get; set; }
        
    }
}
