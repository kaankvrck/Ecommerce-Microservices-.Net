using Ecommerce.Services.OrderAPI.Data;
using Ecommerce.Services.OrderAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Services.OrderAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _context;

        public OrderController(OrderDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreateOrder")]
        public async Task CreateOrder([FromBody] CreateOrderRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                string name = "mert", surname = "Alc", phonenumber = "25354585", email = "dsadsdf@ege.edu.tr", address = " Ankara Turkey ";

                decimal price = 30;

                Order order = new Order()
                {
                    customerid = request.CustomerId,
                    name = name,
                    surname = surname,
                    phonenumber = phonenumber,
                    email = email,
                    address = address,
                    totalprice = price * request.ProductQuantity,
                    statusid = (int)Enums.OrderStatus.Created,
                    createdby = request.CustomerId
                };

                _context.tb_order.Add(order);
                await _context.SaveChangesAsync();

                int orderId = order.id;

                OrderDetail orderDetail = new OrderDetail()
                {
                    orderid = orderId,
                    productid = request.ProductId,
                    quantity = request.ProductQuantity,
                    price = price,
                    createdby = request.CustomerId
                };

                _context.tb_order_detail.Add(orderDetail);
                await _context.SaveChangesAsync();

                // Commit the transaction if everything is successful
                await transaction.CommitAsync();

                Console.WriteLine("Transaction committed successfully.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Transaction rolled back. Error: {ex.Message}");
            }
        }

        [HttpGet("OrderList/{customerId}")]
        public async Task<List<OrderListReponse>> OrderList(int customerId)
        {
            var orderList = await (from order in _context.tb_order
                                   join orderDetail in _context.tb_order_detail on order.id equals orderDetail.orderid
                                   join orderStatus in _context.tb_order_status on order.statusid equals orderStatus.id
                                   where order.customerid == customerId
                                   select new OrderListReponse()
                                   {
                                       CreatedDate = order.createddate,
                                       StatusName = orderStatus.description,
                                       TotalPrice = order.totalprice,
                                       ProductName = "ornek urun",
                                       Quantity = orderDetail.quantity,
                                       Price = orderDetail.price
                                   })
                         .ToListAsync();

            if (orderList == null) return new List<OrderListReponse>();

            return orderList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _context.tb_order
            .FirstOrDefaultAsync(x => x.id == id);

            if (order == null) return NotFound();

            return Ok(order);
        }

    }

}

