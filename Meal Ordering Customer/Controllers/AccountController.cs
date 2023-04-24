using Meal_Ordering_Class_Library.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using Meal_Ordering_Class_Library.ResponseEntities;
using Newtonsoft.Json.Linq;
using Meal_Ordering_Class_Library.Services;

namespace Meal_Ordering_Customer.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly IConfiguration _config;
        private readonly BaseJwtService _baseJwtService;
        public AccountController(IConfiguration config, AccountService accountService, BaseJwtService baseJwtService)
        {
            _config = config;
            _accountService = accountService;
            _baseJwtService = baseJwtService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel model)
        {
            //'Account' Fields were filled in  -> make api request
            if (ModelState.IsValid)
            {
                var response = await _accountService.LoginAsync(model.AccountLoginRequest);
                var responseContent = JObject.Parse(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);

                    // ----START OF SESSION MGMT
                    if (response.Headers.TryGetValues("Authorization", out IEnumerable<string> values))
                    {
                        HttpContext.Session.SetString("Authorization", values.First());

                        if (!await _baseJwtService.CheckCustomerRoleClaimFromToken(HttpContext.Session.GetString("Authorization")))
                        {
                            TempData["ErrorMessage"] = $"(Error) : This account is NOT of type 'Customer'. Please create a 'Customer' account, Thank you.";
                            HttpContext.Session.Clear();
                            return RedirectToAction("Register", "Account");
                        }
                    }
                    
                    // ----END OF SESSION MGMT
                    TempData["LastActionMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
                    return RedirectToAction("Index", "Menu");
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
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(model.AccountRequest.NewPassword) && !string.IsNullOrWhiteSpace(model.AccountRequest.ConfirmNewPassword))
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
                    if (errorsArray != null)
                    {
                        string errorsAsString = "";
                        foreach (JObject errorObject in errorsArray)
                        {
                            //string errorCode = errorObject["code"].ToString();
                            //string errorDescription = errorObject["description"].ToString();
                            errorsAsString += $"{errorObject["description"]}";
                        }
                        TempData["ErrorMessage"] = $"({response.StatusCode}) : {errorsAsString}";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
                    }
                }
                ModelState.AddModelError(string.Empty, "Password fields should be identical in the form.");
                return View(model);
            }
            ModelState.AddModelError(string.Empty, "Please fill out all fields in the form.");
            return View(model);
        }

        [HttpGet("Account/Edit/")]
        public async Task<IActionResult> Edit()
        {
            if (!await _baseJwtService.CheckCustomerRoleClaimFromToken(HttpContext.Session.GetString("Authorization")))
                return RedirectToAction("Login"); // Redirect to the login page if not authenticated

            var response = await _accountService.GetAccountDetailsAsync(HttpContext.Session.GetString("Authorization"));

            AccountViewModel accountViewModel = new AccountViewModel()
            {
                AccountRequest = response
            };
            return View(accountViewModel);
        }

        [HttpPost("Account/Edit/")]
        public async Task<IActionResult> Edit(AccountViewModel model)
        {
            if (!await _baseJwtService.CheckCustomerRoleClaimFromToken(HttpContext.Session.GetString("Authorization")))
            {
                ModelState.AddModelError(string.Empty, "Please login to use this function.");
                return RedirectToAction("Login"); // Redirect to the login page if not authenticated
            }

            if (ModelState.IsValid || (!string.IsNullOrWhiteSpace(model.AccountRequest.CurrentPassword) && !string.IsNullOrWhiteSpace(model.AccountRequest.NewPassword) &&
                !string.IsNullOrWhiteSpace(model.AccountRequest.ConfirmNewPassword)))
            {
                var response = await _accountService.UpdateUserDetailsAsync(model.AccountRequest, HttpContext.Session.GetString("Authorization"));
                var responseContent = JObject.Parse(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    TempData["LastActionMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
                    return RedirectToAction("Index", "Home");
                }
                //create custom tag helper in the future to fix formating
                JArray errorsArray = (JArray)responseContent["errors"];
                if (errorsArray != null)
                {
                    string errorsAsString = "";
                    foreach (JObject errorObject in errorsArray)
                    {
                        //string errorCode = errorObject["code"].ToString();
                        //string errorDescription = errorObject["description"].ToString();
                        errorsAsString += $"{errorObject["description"]}";
                    }
                    TempData["ErrorMessage"] = $"({response.StatusCode}) : {errorsAsString}";
                }
                else
                {
                    TempData["ErrorMessage"] = $"({response.StatusCode}) : {responseContent["message"]}";
                }
                return View(model);
            }
            ModelState.AddModelError(string.Empty, "Please fill out all required fields in the form.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            if (!await _baseJwtService.CheckCustomerRoleClaimFromToken(HttpContext.Session.GetString("Authorization")))
            {
                ModelState.AddModelError(string.Empty, "Please login to use this function.");
                return RedirectToAction("Login"); // Redirect to the login page if not authenticated
            }
            HttpContext.Session.Clear();
            TempData["LastActionMessage"] = $"Thank you for using our service, Have a great day!";
            return RedirectToAction("Index", "Home");
        }
    }
}
