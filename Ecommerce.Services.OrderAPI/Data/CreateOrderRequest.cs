namespace Ecommerce.Services.OrderAPI.Data
{
    public class CreateOrderRequest
    {
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
