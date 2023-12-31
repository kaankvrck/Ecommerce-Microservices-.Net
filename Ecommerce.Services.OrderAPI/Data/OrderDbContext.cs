﻿using System.Collections.Generic;
using Ecommerce.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.OrderAPI.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<Order> tb_order { get; set; }
        public DbSet<OrderDetail> tb_order_detail { get; set; }
        public DbSet<OrderStatus> tb_order_status { get; set; }
    }
}
