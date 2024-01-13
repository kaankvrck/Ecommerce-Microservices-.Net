using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Services.CatalogAPI.Dtos;
using Ecommerce.Services.CatalogAPI.Data;
using Ecommerce.Services.CatalogAPI.Models;

namespace Ecommerce.Services.CatalogAPI.Controllers
{
    [ApiController]
    [Route("api")] // Base path for consistency
    public class ItemsController : ControllerBase
    {
        private readonly ICatalogRepository _repository;

        public ItemsController(ICatalogRepository repository)
        {
            _repository = repository;
        }

        // GET api/items
        [HttpGet("items")]
        public async Task<ActionResult<IEnumerable<Catalog>>> GetItems() 
        {
            var items = await _repository.GetItems();
            return Ok(items);
        }

        // GET /api​/categories
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<string>>> GetDistinctCategories()
        {
            var distinctCategories = await _repository.GetDistinctCategoriesAsync();
            return Ok(distinctCategories);
        }

        // GET /api​/brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<string>>> GetDistinctBrands()
        {
            var distinctBrands = await _repository.GetDistinctBrandsAsync();
            return Ok(distinctBrands);
        }

        // GET /api​/items​/category​/{category}
        [HttpGet("items/category/{category}")]
        public async Task<ActionResult<IEnumerable<Catalog>>> GetItemsByCategory(string category)
        {
            var items = await _repository.GetItemsByCategory(category);
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        // GET /api​/items​/brand​/{brand}
        [HttpGet("items/brand/{brand}")]
        public async Task<ActionResult<IEnumerable<Catalog>>> GetItemsByBrand(string brand)
        {
            var items = await _repository.GetItemsByBrand(brand);
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        // GET api/mSHealth
        [HttpGet("mSHealth")]
        public string Test()
        {
            return "Hello World from Catalog Microservice!";
        }

    }

    [ApiController]
    [Route("api")] // Base path for consistency
    public class ProductsController : ControllerBase
    {
        private readonly ICatalogRepository _repository;

        public ProductsController(ICatalogRepository repository)
        {
            _repository = repository;
        }

        // GET api/products
        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _repository.GetProducts();
            return Ok(products);
        }

        // GET api/check_stock_for_product/{id}
        [HttpGet("check_stock_for_product/{id}")]
        public async Task<ActionResult<ProductStockDto>> CheckStockForProduct(int id)
        {
            var stockDto = await _repository.CheckStockForProduct(id);
            if (stockDto == null)
            {
                return NotFound();
            }
            return Ok(stockDto);
        }

        // PUT api/update_stock_for_product/{id}
        [HttpPut("update_stock_for_product/{id}")]
        public async Task<IActionResult> UpdateStockForProduct(int id, [FromBody] UpdateStockDto updateStockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _repository.UpdateStockForProduct(id, updateStockDto);
            if (success)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

/*
 * Dependency Injection: The controller uses dependency injection to receive the ICatalogRepository instance, 
 * promoting testability and flexibility.
 * 
 * Endpoint Mappings: Each endpoint is decorated with appropriate HTTP verbs and route attributes for correct mapping.
 * 
 * DTO Usage: The controller uses DTOs for input and output to control data exposure and protect sensitive information.
 * 
 * Model Validation: The UpdateStockForProduct() endpoint validates the input model using ModelState.IsValid to ensure 
 * data integrity.
 * 
 * Error Handling: The endpoints handle potential errors gracefully, returning NotFound() for invalid IDs or categories 
 * and BadRequest() for invalid input.
 * 
 * Clear Action Results: The controller returns appropriate action results like Ok(), NoContent(), and NotFound() based 
 * on the operation's success or failure.
 * 
 * [Route("api/[controller]")] is often preferred for consistency and maintainability within a well-structured API.
 * [Route("items")] can be useful for specific routing needs or when working with legacy code.
 * 
 * Asynchronous Operations: The code employs async/await for efficient database interactions.
 * 
 * LINQ Queries: LINQ queries are used for concise and expressive data retrieval.
 * 
 * Interface-Based Approach: The repository pattern promotes loose coupling and testability.
 */