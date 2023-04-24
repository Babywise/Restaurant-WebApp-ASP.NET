using MealOrderingApi.Services;
using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.ResponseEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MealOrderingApi.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController()]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly JwtService _jwtService;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _config;

        public AccountController(IConfiguration config, ILogger<AccountController> logger, SignInManager<User> signInManager, UserManager<User> userManager, JwtService jwtService)
        {
            _config = config;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = await _userManager.FindByNameAsync(await _jwtService.GetClaimValueFromToken(token, "sub"));

            if (user == null)
                return NotFound(new { Message = "User not found" });

            AccountRequest accountRequest = new AccountRequest()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };
            return Ok(accountRequest);
        }

        /// <summary>
        /// Authenticates an existing user
        /// </summary>
        /// <param name="accountLoginRequest"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountLoginRequest accountRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(accountRequest.UserName, accountRequest.CurrentPassword, false, false);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.FindByNameAsync(accountRequest.UserName);
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
                return Unauthorized(new { Message = $"Login failed for user '{accountRequest.UserName}'. Please try again." });
            }
            return BadRequest(new { Message = $"Login failed. Please fill out the username and password and try again." });
        }
        /// <summary>
        /// Registers a new account
        /// </summary>
        /// <param name="accountRegisterRequest"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountRequest accountRequest)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    UserName = accountRequest.UserName,
                    Email = accountRequest.Email,
                    PhoneNumber = accountRequest.PhoneNumber,
                    FirstName = accountRequest.FirstName,
                    LastName = accountRequest.LastName,
                    Address = accountRequest.Address,
                    AccountType = accountRequest.AccountType,
                };
                if (accountRequest.NewPassword != null && accountRequest.ConfirmNewPassword != null)
                {
                    if (accountRequest.NewPassword == accountRequest.ConfirmNewPassword)
                    {
                        var result = await _userManager.CreateAsync(newUser, accountRequest.NewPassword);
                        if (result.Succeeded)
                        {
                            // Set user role based on AccountType
                            string roleName = accountRequest.AccountType == "Customer" ? "Customer" : "Restaurant";
                            await _userManager.AddToRoleAsync(newUser, roleName);
                            return Ok(new
                            {
                                Message = $"User registration for '{newUser.UserName}' as '{roleName}' was successful. " +
                                $"Thank you '{accountRequest.FirstName}' for using our service."
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
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = await _userManager.FindByNameAsync(await _jwtService.GetClaimValueFromToken(token, "sub"));

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            //further validation needs to be done here
            if(!string.IsNullOrWhiteSpace(accountRequest.UserName))
                user.UserName = accountRequest.UserName;
            if(!string.IsNullOrWhiteSpace(accountRequest.Email))
                user.Email = accountRequest.Email;
            if(!string.IsNullOrWhiteSpace(accountRequest.PhoneNumber))
                user.PhoneNumber = accountRequest.PhoneNumber;
            if(!string.IsNullOrWhiteSpace(accountRequest.FirstName))
                user.FirstName = accountRequest.FirstName;
            if(!string.IsNullOrWhiteSpace(accountRequest.LastName))
                user.LastName = accountRequest.LastName;
            if(!string.IsNullOrWhiteSpace(accountRequest.Address))
                user.Address = accountRequest.Address;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            if (!string.IsNullOrEmpty(accountRequest.CurrentPassword) && !string.IsNullOrEmpty(accountRequest.NewPassword))
            {
                var passwordChangeResult = await _userManager.ChangePasswordAsync(user, accountRequest.CurrentPassword, accountRequest.NewPassword);

                if (!passwordChangeResult.Succeeded)
                    return BadRequest(passwordChangeResult);
            }
            return Ok(new { Message = "User details updated successfully" });
        }

    }
}
