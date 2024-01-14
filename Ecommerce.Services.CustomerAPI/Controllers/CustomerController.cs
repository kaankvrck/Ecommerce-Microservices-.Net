using Ecommerce.Services.CustomerAPI.Data;
using Ecommerce.Services.CustomerAPI.Models;
using Ecommerce.Services.CustomerAPI.Models.Dto;
using Ecommerce.Services.CustomerAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Ecommerce.Services.CustomerAPI.Controllers
{

    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerService;
        private readonly IConfiguration _configuration;
        protected ResponseDto _response;

        public CustomerController(ICustomerService customerService, IConfiguration configuration)
        {
            _customerService = customerService;
            _configuration = configuration;
            _response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {

            var errorMessage = await _customerService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _customerService.Login(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);

        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _customerService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);

        }

        [HttpGet("GetPersonalInformation")]
        public async Task<IActionResult> GetPersonalInformation(string customerId)
        {
            var userResponse = await _customerService.GetPersonelInformation(customerId);
            if (userResponse == null)
            {
                _response.IsSuccess = false;
                _response.Message = "User not found!";
                return BadRequest(_response);
            }
            _response.Result = userResponse;
            return Ok(_response);
        }

        [HttpPost("UpdateProfileInformation")]
        public async Task<IActionResult> UpdateProfileInformation([FromBody] UserDto model)
        {
            bool updateProfile = await _customerService.UpdateProfileInformation(model);
            if (!updateProfile)
            {
                _response.IsSuccess = false;
                _response.Message = "Error";
                return BadRequest(_response);
            }
            return Ok(_response);
        }
    }
}
