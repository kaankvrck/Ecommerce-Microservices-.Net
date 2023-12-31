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

        public DbSet<Order> Orders { get; set; }
    }
}
