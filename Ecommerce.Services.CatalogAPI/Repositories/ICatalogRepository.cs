using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ecommerce.Services.CatalogAPI.Dtos;
using Ecommerce.Services.CatalogAPI.Models;

namespace Ecommerce.Services.CatalogAPI.Data
{
    // The ICatalogRepository interface and its implementations are typically responsible for defining methods and
    // logic related to interacting with the data store (e.g., a database), such as CRUD operations on entities.
    public interface ICatalogRepository
    {
        // GetItems() for retrieving all items.
        Task<IEnumerable<Catalog>> GetItems();
        // GetItemById(int id) for retrieving a specific item by ID.
        Task<Catalog> GetItemById(int id);
        // GetDistinctCategoriesAsync() for retrieving all categories distinctly.
        Task<IEnumerable<string>> GetDistinctCategoriesAsync();
        // GetItemsByBrand(string brand) for retrieving all brands distinctly .
        Task<IEnumerable<string>> GetDistinctBrandsAsync();
        // GetItemsByCategory(string category) for filtering items by category.
        Task<IEnumerable<Catalog>> GetItemsByCategory(string category);
       // GetItemsByBrand(string brand) for filtering items by brand.
        Task<IEnumerable<Catalog>> GetItemsByBrand(string brand);
        // CheckStockForProduct(int id) for checking stock of a specific product by id.
        Task<ProductStockDto> CheckStockForProduct(int id);
        // UpdateStockForProduct(int id, UpdateStockDto updateStockDto) for updating stock of a specific product.
        Task<bool> UpdateStockForProduct(int id, UpdateStockDto updateStockDto);
        // GetProducts() for retrieving product information(id, name, price) of a specific product by id.
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
/*
 * Comprehensive CRUD Operations: The interface provides methods for retrieving, filtering, updating, and 
 * fetching distinct values, covering essential data interactions.
 * 
 * Endpoint Alignment: Methods directly map to actions in ItemsController.cs, ensuring a cohesive API structure.
 * 
 * DTO Usage: The CheckStockForProduct() method returns a ProductStockDto to control data exposure (to expose 
 * only necessary data, promoting security and maintainability.), aligning with best practices.
 * 
 * Return Types: The methods return appropriate types (e.g., IEnumerable<Catalog>, ProductStockDto, bool) 
 * to match the expected responses from the controller, ensuring consistency and clarity in data flow.
 * 
 * Asynchronous Operations: Tasks are used for asynchronous database interactions, optimizing performance 
 * and responsiveness.
 * 
 * Interface-Based Design: The repository pattern promotes loose coupling, testability, and maintainability 
 * by abstracting data access logic.
 */

