using Ecommerce.Services.CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.CustomerAPI.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
