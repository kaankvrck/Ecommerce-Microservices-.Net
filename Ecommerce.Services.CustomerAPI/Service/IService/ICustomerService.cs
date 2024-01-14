using Ecommerce.Services.CustomerAPI.Models.Dto;

namespace Ecommerce.Services.CustomerAPI.Service.IService
{
    public interface ICustomerService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
        Task<UserDto> GetPersonelInformation(string customerID);
    }
}
