using Ecommerce.Services.CustomerAPI.Models;

namespace Ecommerce.Services.CustomerAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(CustomerUser applicationUser, IEnumerable<string> roles);
    }
}
