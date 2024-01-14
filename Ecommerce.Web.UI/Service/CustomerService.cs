using Ecommerce.Web.UI.Models;
using Ecommerce.Web.UI.Service.IService;
using Ecommerce.Web.UI.Utility;

namespace Ecommerce.Web.UI.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IBaseService _baseService;
        public CustomerService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.CustomerAPIBase + "/api/customer/AssignRole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = SD.CustomerAPIBase + "/api/customer/login"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.CustomerAPIBase + "/api/customer/register"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> GetPersonalInformation(string customerID)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = customerID,
                Url = SD.CustomerAPIBase + "/api/customer/GetPersonalInformation?customerId="+ customerID
            }, withBearer: false);
        }

        public async Task<ResponseDto?> UpdateProfileInformation(UserDto userDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = userDto,
                Url = SD.CustomerAPIBase + "/api/customer/UpdateProfileInformation"
            }, withBearer: false);
        }
    }
}
