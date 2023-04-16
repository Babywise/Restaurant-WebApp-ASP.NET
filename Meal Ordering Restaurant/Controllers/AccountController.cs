using Meal_Ordering_Class_Library.ResponseEntitiesShared;
using Meal_Ordering_Restaurant.Models;
using Meal_Ordering_Restaurant.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace Meal_Ordering_Restaurant.Controllers
{
    public class AccountController : Controller
    {
        private readonly MealOrderingService _mealOrderingService;
        private readonly IConfiguration _config;

        public AccountController(IConfiguration config)
        {
            _config = config;
            _mealOrderingService = new MealOrderingService("https://localhost:7062");
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
                HttpResponseMessage response = await _mealOrderingService.LoginAsync(model.AccountLoginRequest);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(jsonResponse);
                    string accessToken = (string)jsonObject["accessToken"];
                    HttpContext.Session.SetString("AccessToken", accessToken);
                    HttpContext.Session.SetString("Username", model.AccountLoginRequest.Username);

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
        [HttpGet]
        public IActionResult UpdateDetails()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetails(AccountEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                string accessToken = HttpContext.Session.GetString("AccessToken");

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
