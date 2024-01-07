namespace Ecommerce.Services.OrderAPI.Models
{
    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
