using Ecommerce.Services.CatalogAPI.Data;
using Ecommerce.Services.CatalogAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Ecommerce.Services.CatalogAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {

        private readonly ILogger<CatalogController> _logger;
        private readonly CatalogDbContext _context;

        public CatalogController(
            ILogger<CatalogController> logger,
            CatalogDbContext context
            )
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetAllCatalogs")]
        public async Task<IActionResult> Get()
        {
            var allCatalogs = await _context.Catalogs.ToListAsync();
            return Ok(allCatalogs);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Catalog Catalog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Catalog);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = Catalog.Id }, Catalog);
            }
            return BadRequest(ModelState);
        }
    }
}
