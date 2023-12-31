using Ecommerce.Services.CustomerAPI.Data;
using Ecommerce.Services.CustomerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Ecommerce.Services.CustomerAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerDbContext _context;

        public CustomerController(
            ILogger<CustomerController> logger,
            CustomerDbContext context
            )
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetAllCustomers")]
        public async Task<IActionResult> Get()
        {
            var allCustomers = await _context.Customers.ToListAsync();
            return Ok(allCustomers);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
            }
            return BadRequest(ModelState);
        }
    }
}
