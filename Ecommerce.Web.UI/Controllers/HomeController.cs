using Ecommerce.Web.UI.Models;
using Ecommerce.Web.UI.Service;
using Ecommerce.Web.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace Ecommerce.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenProvider _tokenProvider;

        public HomeController(ILogger<HomeController> logger, ITokenProvider tokenProvider)
        {
            _logger = logger;
            _tokenProvider = tokenProvider;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Shop()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Order()
        {
            if (_tokenProvider.GetToken() == null)
            {
                
                return RedirectToAction("Index", "Home");
            } 
            else
            {
                //!!!!!!!!!!!!!!!!!!!!!!
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(_tokenProvider.GetToken()) as JwtSecurityToken;
                //!!!!!!!!!!!!!!!!!!!
                int quantity = 2;
                decimal price = 3.55m;
                ViewBag.CustomerId = jsonToken.Payload.FirstOrDefault(p => p.Key == "sub").Value.ToString();
                ViewBag.AdSoyad = "Ahmet";
                ViewBag.Address = "İzmir";
                ViewBag.ProductName = "İlaç";
                ViewBag.ProductId = 12542;
                ViewBag.Quantity = quantity;
                ViewBag.Price = price;
                ViewBag.Total = quantity * price;

                return View();
            }
            
        }
        public IActionResult OrderList()
        {
            if (_tokenProvider.GetToken() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //!!!!!!!!!!!!!!!!!!!!!!
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(_tokenProvider.GetToken()) as JwtSecurityToken;
                //!!!!!!!!!!!!!!!!!!!
                string customerId = jsonToken.Payload.FirstOrDefault(p => p.Key == "sub").Value.ToString();
                string requestUrl = "http://localhost:7003/api/orders/OrderList/";
                ViewBag.CustomerId = customerId;
                ViewBag.RequestURL = $"{requestUrl}{customerId}";
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        

    }
}