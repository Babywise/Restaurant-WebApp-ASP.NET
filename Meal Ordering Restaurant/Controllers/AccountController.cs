using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Restaurant.Models;
using Meal_Ordering_Restaurant.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.ResponseEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net.Http.Headers;

namespace Meal_Ordering_Restaurant.Controllers
{
    public class AccountController : Controller
    {
        private readonly MealOrderingService _mealOrderingService;
        private readonly IConfiguration _config;
        public AccountController(IConfiguration config, MealOrderingService mealOrderingService)
        {
            _config = config;
            _mealOrderingService = mealOrderingService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _mealOrderingService.LoginAsync(model.AccountLoginRequest);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);

                    if (response.Headers.TryGetValues("Authorization", out IEnumerable<string> values))
                    {
                        HttpContext.Session.SetString("Authorization", values.First());
                        HttpContext.Session.SetString("Username", loginResponse.Account.Username);
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Login failed");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _mealOrderingService.RegisterAsync(model.AccountRegisterRequest);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Registration failed");
            }

            return View(model);
        }

        [HttpGet("Account/Edit/")]
        public async Task<IActionResult> Edit()
        {
            var response = await _mealOrderingService.GetAccountDetails(HttpContext.Session.GetString("Username"), HttpContext.Session.GetString("Authorization"));

            AccountEditViewModel accountEditViewModel = new AccountEditViewModel()
            {
                AccountEditRequest = response
            };

            return View(accountEditViewModel);
        }

        [HttpPost("Account/Edit/")]
        public async Task<IActionResult> Edit(AccountEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                string accessToken = HttpContext.Session.GetString("Authorization");

                if (string.IsNullOrEmpty(accessToken))
                {
                    return RedirectToAction("Login"); // Redirect to the login page if not authenticated
                }

                var response = await _mealOrderingService.UpdateUserDetailsAsync(model.AccountEditRequest, accessToken);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Updating details failed");
            }

            return View(model);
        }
        [HttpPost]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
