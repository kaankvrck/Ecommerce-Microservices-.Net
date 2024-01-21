namespace Ecommerce.Web.UI.Models
{
    public class CreateOrderRequestDto
    {
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
