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
