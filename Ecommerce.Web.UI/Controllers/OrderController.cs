using Ecommerce.Web.UI.Models;
using Ecommerce.Web.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace Ecommerce.Web.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly ITokenProvider _tokenProvider;

        public OrderController(ICustomerService customerService, IOrderService orderService, ITokenProvider tokenProvider)
        {
            _customerService = customerService;
            _orderService = orderService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var claim = User.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub);

            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                return RedirectToAction("login", "Customer");
            }

            var customerID = claim.Value;

            ResponseDto response = await _customerService.GetPersonalInformation(customerID);

            if (response == null || response.Result == null)
            {
                return NotFound(response.Message ?? "Kullanıcı hesabı bulunamadı!");
            }
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message ?? "Kulanıcı verileri alınırken bir hata oluştu.");
            }

            int quantity = 2;
            decimal price = 3.55m;
            ViewBag.ProductName = "İlaç";
            ViewBag.ProductId = 12542;
            ViewBag.Quantity = quantity;
            ViewBag.Price = price;
            ViewBag.Total = quantity * price;

            UserDto userDto = JsonConvert.DeserializeObject<UserDto>(Convert.ToString(response.Result));
            return View("OrderDetail", userDto);
        }

        [HttpGet]
        public IActionResult MyOrders()
        {
            var claim = User.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub);

            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                return RedirectToAction("login", "Customer");
            }


            var customerId = claim.Value;
            string requestUrl = "http://localhost:7003/api/orders/OrderList/";
            ViewBag.CustomerId = customerId;
            ViewBag.RequestURL = $"{requestUrl}{customerId}";
            return View("MyOrders");

        }

        public async Task<IActionResult> Checkout()
        {
            var claim = User.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub);

            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                return RedirectToAction("login", "Customer");
            }

            var customerID = claim.Value;

            ResponseDto response = await _customerService.GetPersonalInformation(customerID);
            UserDto userDto = JsonConvert.DeserializeObject<UserDto>(Convert.ToString(response.Result));
            CreateOrderRequestDto createOrderRequest = new CreateOrderRequestDto
            {
                PhoneNumber = userDto.PhoneNumber,
                Address = userDto.Address,
                ProductId = 12542,
                ProductQuantity = 2

            };
            ResponseDto responseDto = await _orderService.CreateOrderAsync(createOrderRequest);

            if (responseDto != null && responseDto.IsSuccess)
            {
                return RedirectToAction("Index", "MyOrders");
            }
            else
            {
                return RedirectToPage("/BadRequest");
            }


        }




    }
}
