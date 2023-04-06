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
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/Account/Register?Username=Danny&&Password=Danny12&&AccountType=Customer&&Address=1234Testing") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/text";
            request.UserAgent = "StubTest";
            HttpWebResponse response=null;
            //Response from api
            try
            {
               response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex) {
            }
            string data = Classes.Responses.getKeyFromResponse(response, "Message");
          //  LoginResponse resp = Classes.Responses.getLoginResponseFromResponse(response); // gets account from string
        
            return View();
        }

    }
}