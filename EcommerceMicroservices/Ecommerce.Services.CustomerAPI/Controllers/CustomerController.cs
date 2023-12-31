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
            var customer = new Customer()
            {
                Name = "Bradley Browning",
                Email = "jason20@hamilton-valdez.org",
                Phone = "(658)620 - 6936x0840",
                Address = "05346 Terry Causeway Suite 323, South Petermouth, AK 80388",
                City = "Ashleyshire",
                Region = "Wisconsin",
                PostalCode = "53407"
            };

            _context.Add(customer);
            await _context.SaveChangesAsync();

            var allCustomers = await _context.Customers.ToListAsync();

            return Ok(allCustomers);
        }
    }
}
