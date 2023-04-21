using Meal_Ordering_Class_Library.Models;
using Meal_Ordering_Restaurant.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using Meal_Ordering_Class_Library.ResponseEntities;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

namespace Meal_Ordering_Restaurant.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly IConfiguration _config;
        public AccountController(IConfiguration config, AccountService accountService)
        {
            _config = config;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountViewModel model)
        {
            //'Account' Fields were filled in  -> make api request
            if (!string.IsNullOrWhiteSpace(model.AccountRequest.Account.UserName) &&
                !string.IsNullOrWhiteSpace(model.AccountRequest.Account.CurrentPassword))
            {
                var response = await _accountService.LoginAsync(model.AccountRequest);
                var responseContent = JObject.Parse(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);

                    // ----START OF SESSION MGMT
                    if (response.Headers.TryGetValues("Authorization", out IEnumerable<string> values))
                    {
                        HttpContext.Session.SetString("Authorization", values.First());
                        HttpContext.Session.SetString("Username", loginResponse.Account.UserName);
                    }
                    // ----END OF SESSION MGMT
                    TempData["LastActionMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
                    return RedirectToAction("Index", "Management");
                }
                TempData["ErrorMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
                return View(model);
            }
            ModelState.AddModelError(string.Empty, "Please enter a username and password.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.AccountRequest.Account.UserName) && !string.IsNullOrWhiteSpace(model.AccountRequest.Account.FirstName) &&
                !string.IsNullOrWhiteSpace(model.AccountRequest.Account.LastName) && !string.IsNullOrWhiteSpace(model.AccountRequest.Account.Email) &&
                !string.IsNullOrWhiteSpace(model.AccountRequest.Account.PhoneNumber) && !string.IsNullOrWhiteSpace(model.AccountRequest.Account.Address) &&
                !string.IsNullOrWhiteSpace(model.AccountRequest.Account.AccountType))
            {
                var response = await _accountService.RegisterAsync(model.AccountRequest);
                var responseContent = JObject.Parse(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    TempData["LastActionMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
                    return RedirectToAction("Index", "Home");
                }
                //create custom tag helper in the future to fix formating
                JArray errorsArray = (JArray)responseContent["errors"];
                string errorsAsString = "";
                foreach (JObject errorObject in errorsArray)
                {
                    //string errorCode = errorObject["code"].ToString();
                    //string errorDescription = errorObject["description"].ToString();
                    errorsAsString += $"{errorObject["description"]}";
                }
                TempData["ErrorMessage"] = $"({response.StatusCode}) : {errorsAsString}";
                return View(model);
            }
            ModelState.AddModelError(string.Empty, "Please fill out all fields in the form.");
            return View(model);
        }

        [HttpGet("Account/Edit/")]
        public async Task<IActionResult> Edit()
        {
            var response = await _accountService.GetAccountDetailsAsync(HttpContext.Session.GetString("Username"), HttpContext.Session.GetString("Authorization"));

            AccountViewModel accountViewModel = new AccountViewModel()
            {
                AccountRequest = response
            };
            return View(accountViewModel);
        }

        [HttpPost("Account/Edit/")]
        public async Task<IActionResult> Edit(AccountViewModel model)
        {
            if ((!string.IsNullOrWhiteSpace(model.AccountRequest.Account.UserName) && !string.IsNullOrWhiteSpace(model.AccountRequest.Account.FirstName) &&
                !string.IsNullOrWhiteSpace(model.AccountRequest.Account.LastName) && !string.IsNullOrWhiteSpace(model.AccountRequest.Account.Email) &&
                !string.IsNullOrWhiteSpace(model.AccountRequest.Account.PhoneNumber) && !string.IsNullOrWhiteSpace(model.AccountRequest.Account.Address)) ||
                (!string.IsNullOrWhiteSpace(model.AccountRequest.Account.CurrentPassword) && !string.IsNullOrWhiteSpace(model.AccountRequest.Account.NewPassword) &&
                !string.IsNullOrWhiteSpace(model.AccountRequest.Account.ConfirmNewPassword)))
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("Authorization")))
                    return RedirectToAction("Login"); // Redirect to the login page if not authenticated

                if (string.IsNullOrWhiteSpace(model.AccountRequest.Account.UserName))
                    model.AccountRequest.Account.UserName = HttpContext.Session.GetString("Username");

                var response = await _accountService.UpdateUserDetailsAsync(model.AccountRequest, HttpContext.Session.GetString("Authorization"));
                var responseContent = JObject.Parse(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    TempData["LastActionMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
                    return RedirectToAction("Index", "Home");
                }
                //create custom tag helper in the future to fix formating
                JArray errorsArray = (JArray)responseContent["errors"];
                string errorsAsString = "";
                foreach (JObject errorObject in errorsArray)
                {
                    //string errorCode = errorObject["code"].ToString();
                    //string errorDescription = errorObject["description"].ToString();
                    errorsAsString += $"{errorObject["description"]}";
                }
                TempData["ErrorMessage"] = $"({response.StatusCode}) : {errorsAsString}";
                return View(model);
            }
            ModelState.AddModelError(string.Empty, "Please fill out all fields in the form.");
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
