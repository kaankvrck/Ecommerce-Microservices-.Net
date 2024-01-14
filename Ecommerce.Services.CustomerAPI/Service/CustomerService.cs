
using Ecommerce.Services.CustomerAPI.Data;
using Ecommerce.Services.CustomerAPI.Models;
using Ecommerce.Services.CustomerAPI.Models.Dto;
using Ecommerce.Services.CustomerAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Services.CustomerAPI.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerDbContext _db;
        private readonly UserManager<CustomerUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public CustomerService(CustomerDbContext db, IJwtTokenGenerator jwtTokenGenerator,
            UserManager<CustomerUser> userManager, RoleManager<IdentityRole> roleManager)
        {
                _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.CustomerUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;

        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.CustomerUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user,loginRequestDto.Password);

            if(user==null || isValid == false)
            {
                return new LoginResponseDto() { User = null,Token="" };
            }

            //if user was found , Generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user,roles);

            UserDto userDTO = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,

            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDTO,
                Token = token
            };

            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            CustomerUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber,
                Address = registrationRequestDto.Address,
            };

            try
            {
                var result =await  _userManager.CreateAsync(user,registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.CustomerUsers.First(u => u.UserName == registrationRequestDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber,
                        Address = userToReturn.Address

                    };

                    return "";

                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }

            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }

        public async Task<UserDto> GetPersonelInformation(string customerID)
        {
            var user = _db.CustomerUsers.FirstOrDefault(u => u.Id == customerID);
            if (user != null)
            {
                UserDto userDto = new()
                {
                    Email = user.Email,
                    ID = user.Id,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address
                };
                return userDto;
            }
            return null;
        }

        public async Task<bool> UpdateProfileInformation(UserDto model)
        {
            var user = _db.CustomerUsers.FirstOrDefault(u => u.Id == model.ID);
            if (user != null)
            {
                user.Email = model.Email;
                user.Name = model.Name;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;

                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
