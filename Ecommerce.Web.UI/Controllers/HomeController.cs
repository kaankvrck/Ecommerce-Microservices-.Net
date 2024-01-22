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
        public IActionResult GetItems()
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
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        

    }
}