using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Services.CatalogAPI.Data;
using Ecommerce.Services.CatalogAPI.Dtos;
using Ecommerce.Services.CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.CatalogAPI.Data
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly CatalogDbContext _context;
        public CatalogRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Catalog>> GetItems()
        {
            return await _context.tb_catalog.ToListAsync();
        }

        public async Task<IEnumerable<string>> GetDistinctBrandsAsync()
        {
            return await _context.tb_catalog
                                 .Select(c => c.brand)
                                 .Distinct()
                                 .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetDistinctCategoriesAsync()
        {
            return await _context.tb_catalog
                                 .Select(c => c.category)
                                 .Distinct()
                                 .ToListAsync();
        }

        public async Task<Catalog> GetItemById(int id)
        {
            return await _context.tb_catalog.FindAsync(id);
        }

        public async Task<IEnumerable<Catalog>> GetItemsByCategory(string category)
        {
            return await _context.tb_catalog.Where(c => c.category == category).ToListAsync();
        }

        public async Task<IEnumerable<Catalog>> GetItemsByBrand(string brand)
        {
            return await _context.tb_catalog.Where(c => c.brand == brand).ToListAsync();
        }

        public async Task<ProductStockDto> CheckStockForProduct(int id)
        {
            var catalogItem = await _context.tb_catalog.FindAsync(id);
            if (catalogItem != null)
            {
                return new ProductStockDto(catalogItem.productid, catalogItem.stock);
            }
            else
            {
                return null; // Or throw an exception if appropriate
            }
        }

        public async Task<bool> UpdateStockForProduct(int id, UpdateStockDto updateStockDto)
        {
            var catalogItem = await _context.tb_catalog.FindAsync(id);
            if (catalogItem != null)
            {
                catalogItem.stock = updateStockDto.stock;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false; // Or throw an exception if appropriate
            }
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            return await _context.tb_catalog.Select(c => new ProductDto(c.productid, c.name, c.price)).ToListAsync();
        }
    }
}
/*
 * Method Implementations: Each method in the interface is implemented using Entity Framework Core's 
 * query and update mechanisms.
 * 
 * Error Handling: Consider adding proper error handling (e.g., using try-catch blocks) to handle 
 * potential exceptions during database operations.
 */