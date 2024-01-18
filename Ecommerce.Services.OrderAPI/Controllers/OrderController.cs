using Ecommerce.Services.OrderAPI.Common;
using Ecommerce.Services.OrderAPI.Data;
using Ecommerce.Services.OrderAPI.Models;
using Ecommerce.Services.OrderAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Services.OrderAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _context;
        private readonly ApiServiceHelper _apiServiceHelper;
        protected ResponseDto _response;

        public OrderController(OrderDbContext context, ApiServiceHelper apiServiceHelper)
        {
            _context = context;
            _apiServiceHelper = apiServiceHelper;
            _response = new();
        }

        [HttpPost("CreateOrder")]
        //[Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();

            var tokenPayload = JwtTokenHelper.GetJwtPayload(authorizationHeader);
            if (tokenPayload != null) 
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    decimal price = 30;
                    // check stock ıf success return ok else throw exceptıon
                    var customerId = tokenPayload.FirstOrDefault(p => p.Key == "sub").Value.ToString();
                    Order order = new Order()
                    {
                        customerid = customerId,
                        name = tokenPayload.FirstOrDefault(p => p.Key == "name").Value.ToString(),
                        phonenumber = request.PhoneNumber,
                        email = tokenPayload.FirstOrDefault(p => p.Key == "email").Value.ToString(),
                        address = request.Address,
                        totalprice = price * request.ProductQuantity,
                        statusid = (int)Enums.OrderStatus.Created,
                        createdby = customerId
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
                        createdby = customerId
                    };

                    _context.tb_order_detail.Add(orderDetail);
                    await _context.SaveChangesAsync();

                    // update stock ıf not success throw exceptıon 
                    // Commit the transaction if everything is successful
                    await transaction.CommitAsync();
                    return Ok(_response);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _response.IsSuccess = false;
                    _response.Message = ex.Message;
                    return BadRequest(_response);
                }
            }
            else
            {
                return BadRequest(_response);
            }
        }

        [HttpGet("OrderList/{customerId}")]
        public async Task<List<OrderListReponse>> OrderList(string customerId)
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

