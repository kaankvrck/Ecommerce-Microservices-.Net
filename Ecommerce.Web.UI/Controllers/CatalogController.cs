using Ecommerce.Web.UI.Models;
using Ecommerce.Web.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Ecommerce.Web.UI.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ICatalogService _catalogService;
        private readonly ITokenProvider _tokenProvider;

        public CatalogController(ICustomerService customerService, ICatalogService catalogService, ITokenProvider tokenProvider)
        {
            _customerService = customerService;
            _catalogService = catalogService;
            _tokenProvider = tokenProvider;
        }

        //[HttpGet]
        //public IActionResult GetItems()
        //{
        //    List <CatalogDto> catalogList = new();
        //    return View(catalogList);
        //}
        [HttpGet("items")]
        public async Task<IActionResult> GetItems()
        {

            ResponseDto response = await _catalogService.GetItems();
            if (response == null || response.Result == null)
            {
                return NotFound(response.Message ?? "Catalog or result not found!");
            }
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message ?? "An error occurred while fetching user data.");
            }

            List<CatalogDto> items = JsonConvert.DeserializeObject<List<CatalogDto>>(Convert.ToString(response.Result));
            return View("GetItems", items);

        }
        public IActionResult NavigateTo(string[] isim, string[] productid, int[] adet, string[] price, string choosenid)
        {

            int index = Convert.ToInt32(choosenid) - 1;

            TempData["productId"] = productid[index];
            TempData["quantity"] = adet[index];
            TempData["price"] = price[index];
            TempData["productName"] = isim[index];

            return RedirectToAction("Details", "Order", new
            {
                productId = TempData["productId"],
                quantity = TempData["quantity"],
                price = TempData["price"],
                productName = TempData["productName"]
            });

        }
    }
}
