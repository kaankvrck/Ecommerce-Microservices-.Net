using Ecommerce.Web.UI.Models;
using Ecommerce.Web.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;

using System.Net.Http.Json;
using System.Text.Json;

namespace Ecommerce.Web.UI.Controllers
{
    public class CatalogController : Controller
    {
        public ActionResult NavigateToCreateOrder()
        {
            // Your login logic here using the parameters

            // Store data in TempData
            TempData["productId"] = 1;
            TempData["quantity"] = 3;
            TempData["price"] = 30;
            TempData["productName"] = "Mineral";

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
