using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.UI.Models
{
    public record ItemDto(
        int productid,
        string name,
        string category,
        string brand,
        string description,
        int stock,
        decimal price,
        string image
    );

    // Not used for the final scope of the project
    public record CreateItemDto(
        [Required] string name,
        string category,
        string brand,
        string description,
        [Required] int stock,
        [Required][Range(0, 1000)] decimal price,
        string image
    );

    public record ProductDto(
        int id,
        string name,
        decimal price
    );

    public record ProductStockDto(
        int id,
        int stock
    );

    public record UpdateStockDto(
        int id,
        [Required][Range(0, 1000000)] int stock
    );
}

/*
 * Clear and Concise: The DTOs represent specific data structures for exchanging information between 
 * layers, promoting clarity and maintainability.
 * 
 * Appropriate Properties: Each DTO includes only the necessary properties for its intended purpose, 
 * reducing data exposure and potential security risks.
 * 
 * Data Validation: The CreateItemDto and UpdateStockDto leverage data annotations for validation, 
 * ensuring data integrity and preventing invalid inputs.
 * 
 * Type Safety: Records are used for immutability and type safety, enhancing code reliability and 
 * reducing potential errors.
 * 
 * Naming Conventions: Consistent naming conventions (e.g., ProductDto for product-related data) 
 * improve readability and understanding.
 */