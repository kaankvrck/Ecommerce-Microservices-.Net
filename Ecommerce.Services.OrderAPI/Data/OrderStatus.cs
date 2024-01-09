namespace Ecommerce.Services.OrderAPI.Data
{
    public class OrderStatus
    {
        public int id { get; set; }
        public string description { get; set; }
        public DateTime createddate { get; set; }
        public int createdby { get; set; }
        public DateTime? modifieddate { get; set; }
        public int? modifiedby { get; set; }
        public bool isdeleted { get; set; }
    }
}
