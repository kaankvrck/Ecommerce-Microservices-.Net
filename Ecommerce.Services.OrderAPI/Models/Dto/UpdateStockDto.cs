using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Services.OrderAPI.Models.Dto
{

    public record UpdateStockDto(
        int id,
        [Required][Range(0, 1000000)] int stock
    );


}
