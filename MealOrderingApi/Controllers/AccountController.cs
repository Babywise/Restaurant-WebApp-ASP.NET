using MealOrderingApi.Services;
using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.ResponseEntities;
using Meal_Ordering_Class_Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure.Core;

namespace MealOrderingApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController()]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _config;

        public AccountController(IConfiguration config, ILogger<AccountController> logger, SignInManager<User> signInManager, UserManager<User> userManager, IJwtService jwtService)
        {
            _config = config;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Authenticates an existing user
        /// </summary>
        /// <param name="accountLoginRequest"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountLoginRequest accountLoginRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(accountLoginRequest.Username, accountLoginRequest.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.FindByNameAsync(accountLoginRequest.Username);
                var userId = appUser.Id;
                var jwtToken = await _jwtService.GenerateJwtToken(appUser);

                Response.Headers.Add("Authorization", jwtToken);

                LoginResponse loginResponse = new LoginResponse()
                {
                    Account = new Account
                    {
                        UserId = userId,
                        AccessToken = jwtToken,
                        FirstName = appUser.FirstName,
                        LastName = appUser.LastName,
                        AccountType = appUser.AccountType,
                        Email = appUser.Email,
                        Address = appUser.Address,
                        Phone = appUser.PhoneNumber,
                        Username = appUser.UserName
                    },
                    
                };

                return Ok(loginResponse);
            }

            return Unauthorized();
        }
        /// <summary>
        /// Registers a new account
        /// </summary>
        /// <param name="accountRegisterRequest"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountRegisterRequest accountRegisterRequest)
        {
            if (ModelState.IsValid)
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
                    // Set user role based on AccountType
                    string roleName = accountRegisterRequest.AccountType == "Customer" ? "Customer" : "Restaurant";
                    await _userManager.AddToRoleAsync(newUser, roleName);
                    return Ok(new { Message = "User registration successful" });
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(new { Message = "There are errors in the registration form." });
        }
        /// <summary>
        /// Modifys an existing account details
        /// </summary>
        /// <param name="accountEditRequest"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpPut("edit")]
        [Authorize]
        public async Task<IActionResult> Edit([FromBody] AccountEditRequest accountEditRequest)
        {
            var user = await _userManager.FindByIdAsync(accountEditRequest.Username);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            //further validation can be done here
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

        //[Authorize]
        [HttpGet("edit")]
        [Authorize]
        public async Task<IActionResult> Edit(string Username)
        {
            var user = await _userManager.FindByNameAsync(Username);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            AccountEditRequest accountEditRequest = new AccountEditRequest() 
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };

            return Ok(accountEditRequest);
        }
    }
}
