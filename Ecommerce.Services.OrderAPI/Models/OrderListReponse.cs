﻿namespace Ecommerce.Services.OrderAPI.Models
{
    public class OrderListReponse
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
