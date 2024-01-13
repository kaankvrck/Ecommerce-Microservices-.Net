using Ecommerce.Services.OrderAPI.Common;

namespace Ecommerce.Services.OrderAPI.Models
{
    public class OrderDetail
    {
        public int id { get; set; }
        public int orderid { get; set; }
        public int productid { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public DateTime createddate { get; set; } = DateTime.SpecifyKind(TimeHelper.GetCurrentTurkeyTime(), DateTimeKind.Utc);
        public string createdby { get; set; }
        public DateTime? modifieddate { get; set; }
        public string? modifiedby { get; set; }
        public bool isdeleted { get; set; }
    }
}
