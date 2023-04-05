using Azure;
using Meal_Ordering_API.Entities;
using Meal_Ordering_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Meal_Ordering_API.Classes;
namespace Meal_Ordering_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // create request to register test
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/Account/Login?Username='Danny'&&Password='Danny'&&AccountType='Resteraunt'") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/text";
            request.UserAgent = "StubTest";
            //Response from api
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            LoginResponse resp = Classes.Responses.getLoginResponseFromResponse(response); // gets account from string
        
            return View();
        }

    }
}