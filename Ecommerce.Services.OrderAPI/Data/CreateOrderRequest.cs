﻿namespace Ecommerce.Services.OrderAPI.Data
{
    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
