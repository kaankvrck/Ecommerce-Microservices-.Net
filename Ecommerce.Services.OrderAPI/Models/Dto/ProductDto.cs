namespace Ecommerce.Services.OrderAPI.Models.Dto
{
    public record ProductDto(
        int id,
        string name,
        decimal price
    );
}
