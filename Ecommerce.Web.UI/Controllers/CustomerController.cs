using Ecommerce.Web.UI.Models;
using Ecommerce.Web.UI.Service;
using Ecommerce.Web.UI.Service.IService;
using Ecommerce.Web.UI.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;

namespace Ecommerce.Web.UI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ITokenProvider _tokenProvider;

        public CustomerController(ICustomerService authService, ITokenProvider  tokenProvider)
        {
            _customerService = authService;
            _tokenProvider = tokenProvider; 
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            ResponseDto responseDto = await _customerService.LoginAsync(obj);

            if (responseDto != null && responseDto.IsSuccess)
            {
                LoginResponseDto loginResponseDto = 
                    JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

                await SignInUser(loginResponseDto);
                _tokenProvider.SetToken(loginResponseDto.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = responseDto.Message;
                return View(obj);
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer},
            };

            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            ResponseDto result = await _customerService.RegisterAsync(obj);
            ResponseDto assingRole;

            if(result!=null && result.IsSuccess)
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.RoleCustomer;
                }
                assingRole = await _customerService.AssignRoleAsync(obj);
                if (assingRole!=null && assingRole.IsSuccess)
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["error"] = result.Message;
            }

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer},
            };

            ViewBag.RoleList = roleList;
            return View(obj);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonalInformation()
        {
            var claim = User.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub);

            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                return RedirectToAction("login", "Customer");
            }

            var customerID = claim.Value;

            ResponseDto response = await _customerService.GetPersonalInformation(customerID);
            if (response == null || response.Result == null)
            {
                return NotFound(response.Message ?? "User or result not found!");
            }
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message ?? "An error occurred while fetching user data.");
            }

            UserDto userDto = JsonConvert.DeserializeObject<UserDto>(Convert.ToString(response.Result));
            return View("Profile", userDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfileInformation(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto response = await _customerService.UpdateProfileInformation(userDto);
                if (response != null && response.IsSuccess)
                {
                    // Örneğin, başarılı bir mesaj göstermek veya başka bir sayfaya yönlendirmek
                    return RedirectToAction("SuccessPage");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Profil güncellenirken bir hata oluştu.");
                }
            }
            return View(userDto);
        }

        private async Task SignInUser(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, 
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));


            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role,
                jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));



            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

    }
}
