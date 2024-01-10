using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Services.CustomerAPI.Models
{
    public class CustomerUser : IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}