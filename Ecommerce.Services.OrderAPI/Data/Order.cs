using Ecommerce.Services.OrderAPI.Common;

namespace Ecommerce.Services.OrderAPI.Data
{
    public class Order
    {
        public int id { get; set; }
        public int customerid { get; set; }
        public string? name { get; set; }
        public string? surname { get; set; }
        public string? phonenumber { get; set; }
        public string? email { get; set; }
        public string? address { get; set; }
        public decimal totalprice { get; set; }
        public int statusid { get; set; }
        public DateTime createddate { get; set; } = DateTime.SpecifyKind(TimeHelper.GetCurrentTurkeyTime(), DateTimeKind.Utc);
        public int createdby { get; set; }
        public DateTime? modifieddate { get; set; }
        public int? modifiedby { get; set; }
        public bool isdeleted { get; set; }
    }


}
