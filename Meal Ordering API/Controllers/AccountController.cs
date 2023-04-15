using Meal_Ordering_API.Services;
using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.ResponseEntitiesShared;
using Meal_Ordering_Class_Library.Services;
using Meal_Ordering_Restaurant.RequestEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Meal_Ordering_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController()]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;

        public AccountController(IConfiguration config, SignInManager<User> signInManager, UserManager<User> userManager, IJwtService jwtService)
        {
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] AccountLoginRequest accountLoginRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(accountLoginRequest.Username, accountLoginRequest.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.FindByNameAsync(accountLoginRequest.Username);
                var token = _jwtService.GenerateJwtToken(appUser);
                return Ok(new { AccessToken = token });
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountRegisterRequest accountRegisterRequest)
        {
            var newUser = new User
            {
                UserName = accountRegisterRequest.Username,
                Email = accountRegisterRequest.Email,
                PhoneNumber = accountRegisterRequest.PhoneNumber,
                FirstName = accountRegisterRequest.FirstName,
                LastName = accountRegisterRequest.LastName,
                AccountType = accountRegisterRequest.AccountType,
            };

            var result = await _userManager.CreateAsync(newUser, accountRegisterRequest.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User registration successful" });
            }

            return BadRequest(result.Errors);
        }

        [Authorize]
        [HttpPut("edit")]
        public async Task<IActionResult> EditDetails([FromBody] AccountEditRequest accountEditRequest)
        {
            var user = await _userManager.FindByIdAsync(accountEditRequest.AccountId.ToString());
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            user.Email = accountEditRequest.Email;
            user.PhoneNumber = accountEditRequest.PhoneNumber;
            user.FirstName = accountEditRequest.FirstName;
            user.LastName = accountEditRequest.LastName;
            user.Address = accountEditRequest.Address;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (!string.IsNullOrEmpty(accountEditRequest.CurrentPassword) && !string.IsNullOrEmpty(accountEditRequest.NewPassword))
            {
                var passwordChangeResult = await _userManager.ChangePasswordAsync(user, accountEditRequest.CurrentPassword, accountEditRequest.NewPassword);

                if (!passwordChangeResult.Succeeded)
                {
                    return BadRequest(passwordChangeResult.Errors);
                }
            }

            return Ok(new { Message = "User details updated successfully" });
        }
    }
}
