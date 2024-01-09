using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.CustomerAPI.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
            
        }

    }
}
