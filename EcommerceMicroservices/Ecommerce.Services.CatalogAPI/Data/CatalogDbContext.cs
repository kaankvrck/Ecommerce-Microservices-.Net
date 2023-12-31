using Ecommerce.Services.CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.CatalogAPI.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Catalog> Catalogs { get; set; }
    }
}
