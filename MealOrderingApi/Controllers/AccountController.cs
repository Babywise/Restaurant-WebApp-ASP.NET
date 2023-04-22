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
    [Route("api/v2/[controller]")]
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index(string Username)
        {
            var user = await _userManager.FindByNameAsync(Username);

            if (user == null)
                return NotFound(new { Message = "User not found" });

            AccountRequest accountRequest = new AccountRequest()
            {
                Account = new Account()
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address
                }
            };
            return Ok(accountRequest);
        }

        /// <summary>
        /// Authenticates an existing user
        /// </summary>
        /// <param name="accountLoginRequest"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountRequest accountRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(accountRequest.Account.UserName, accountRequest.Account.CurrentPassword, false, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.FindByNameAsync(accountRequest.Account.UserName);
                var userId = appUser.Id;
                var jwtToken = await _jwtService.GenerateJwtToken(appUser);

                Response.Headers.Add("Authorization", jwtToken);

                LoginResponse loginResponse = new LoginResponse()
                {
                    Account = new Account
                    {
                        FirstName = appUser.FirstName,
                        LastName = appUser.LastName,
                        AccountType = appUser.AccountType,
                        Email = appUser.Email,
                        Address = appUser.Address,
                        PhoneNumber = appUser.PhoneNumber,
                        UserName = appUser.UserName
                    },
                };
                var response = new
                {
                    Account = loginResponse.Account,
                    Message = $"Login Successful. Thank you '{loginResponse.Account.FirstName}' for using our service."
                };
                return Ok(response);
            }
            return Unauthorized(new { Message = $"Login failed for user '{accountRequest.Account.UserName}'. Please try again."});
        }
        /// <summary>
        /// Registers a new account
        /// </summary>
        /// <param name="accountRegisterRequest"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountRequest accountRequest)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    UserName = accountRequest.Account.UserName,
                    Email = accountRequest.Account.Email,
                    PhoneNumber = accountRequest.Account.PhoneNumber,
                    FirstName = accountRequest.Account.FirstName,
                    LastName = accountRequest.Account.LastName,
                    AccountType = accountRequest.Account.AccountType,
                };
                if (accountRequest.Account.NewPassword != null && accountRequest.Account.ConfirmNewPassword != null)
                {
                    if (accountRequest.Account.NewPassword == accountRequest.Account.ConfirmNewPassword)
                    {
                        var result = await _userManager.CreateAsync(newUser, accountRequest.Account.NewPassword);
                        if (result.Succeeded)
                        {
                            // Set user role based on AccountType
                            string roleName = accountRequest.Account.AccountType == "Customer" ? "Customer" : "Restaurant";
                            await _userManager.AddToRoleAsync(newUser, roleName);
                            return Ok(new
                            {
                                Message = $"User registration for '{newUser.UserName}' as '{roleName}' was successful. " +
                                $"Thank you '{accountRequest.Account.FirstName}' for using our service."
                            });
                        }
                        return BadRequest(result);
                    }
                    return BadRequest(new { Message = "Passwords do not match." });
                }
                return BadRequest(new { Message = "Please fill out both password fields." });
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
        public async Task<IActionResult> Edit([FromBody] AccountRequest accountRequest)
        {
            var user = await _userManager.FindByNameAsync(accountRequest.Account.UserName);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            //further validation needs to be done here
            if(!string.IsNullOrWhiteSpace(accountRequest.Account.Email))
                user.Email = accountRequest.Account.Email;
            if(!string.IsNullOrWhiteSpace(accountRequest.Account.PhoneNumber))
                user.PhoneNumber = accountRequest.Account.PhoneNumber;
            if(!string.IsNullOrWhiteSpace(accountRequest.Account.FirstName))
                user.FirstName = accountRequest.Account.FirstName;
            if(!string.IsNullOrWhiteSpace(accountRequest.Account.LastName))
                user.LastName = accountRequest.Account.LastName;
            if(!string.IsNullOrWhiteSpace(accountRequest.Account.Address))
                user.Address = accountRequest.Account.Address;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            if (!string.IsNullOrEmpty(accountRequest.Account.CurrentPassword) && !string.IsNullOrEmpty(accountRequest.Account.NewPassword))
            {
                var passwordChangeResult = await _userManager.ChangePasswordAsync(user, accountRequest.Account.CurrentPassword, accountRequest.Account.NewPassword);

                if (!passwordChangeResult.Succeeded)
                    return BadRequest(passwordChangeResult);
            }
            return Ok(new { Message = "User details updated successfully" });
        }

    }
}
