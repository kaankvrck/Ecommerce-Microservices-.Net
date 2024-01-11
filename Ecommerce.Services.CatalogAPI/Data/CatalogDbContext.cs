using Ecommerce.Services.CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.CatalogAPI.Data
{
    //constructor
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Catalog> tb_catalog { get; set; }
    }
}
