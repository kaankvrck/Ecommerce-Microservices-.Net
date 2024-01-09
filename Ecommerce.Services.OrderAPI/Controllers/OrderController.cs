using Ecommerce.Services.OrderAPI.Common;
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
        //ApiService için eklendi
        private readonly ApiServiceHelper _apiServiceHelper;

        public OrderController(OrderDbContext context, ApiServiceHelper apiServiceHelper)
        {
            _context = context;
            //ApiService için eklendi
            _apiServiceHelper = apiServiceHelper;
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


        [HttpGet("active")]
        public async Task<ActionResult<List<Order>>> GetActiveOrders()
        {
            var activeOrders = await (from order in _context.tb_order
                                      where order.statusid != 4
                                      select order).ToListAsync();

            if (activeOrders == null) return NotFound();

            return activeOrders;
        }
        

        //Sipariş durumunu "İptal edildi" yapma
        [HttpPost("{OrderId}")]
        public async Task<ActionResult<Order>> UpdateOrderStatus(int orderId)
        {

            var order = await _context.tb_order
            .FirstOrDefaultAsync(x => x.id == orderId);

            if (order == null) return NotFound();

            if (order.statusid == 4)
            {
                Console.WriteLine("Sipariş zaten iptal edilmiş durumdadır!");
                return Ok(order);
            }

            else
            {
                order.statusid = 4;
                Console.WriteLine("Sipariş İptal edildi!");

                _context.SaveChanges();

                return Ok(order);
            }
        }




        //Just for testing
        [HttpGet("external")]
        public async Task<ActionResult> GetHttpApiData()
        {
            string apiUrl = "https://reqres.in/api/users/2";

            string getData = await _apiServiceHelper.GetAsync(apiUrl);

            return Ok(getData);
        }

        [HttpGet("param")]
        public async Task<ActionResult> GetHttpApiDataWithParam()
        {
            string apiUrl = "https://reqres.in/api/users";
            string param = "2";

            string getData = await _apiServiceHelper.GetWithParamAsync(apiUrl, param);

            return Ok(getData);
        }



    }

}

