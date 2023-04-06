using Azure;
using Meal_Ordering_API.Entities;
using Meal_Ordering_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Meal_Ordering_API.Classes;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Meal_Ordering_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult AddProduct()
        {
           
            // create request to register test
            string link = "https://localhost:7062/API/V1/StoreManagement/Add?";
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/StoreManagement/Add") as HttpWebRequest;
            request.Method = "POST";
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.UserAgent = "StubTest";
            request.Headers.Add("ApiKey", "{1a07e8f6-825c-442d-a3d7-1315e6780697}");
            Product product = new Product()
            {
                Cost = 1,
                Name = "Test",
                CategoryId = 0
            };
            string stringData = JsonSerializer.Serialize(product); // place body here
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] bytes = encoding.GetBytes(stringData);

            Stream newStream = request.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();


            HttpWebResponse response = null;
            //Response from api
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
            }
            string data = Classes.Responses.getKeyFromResponse(response, "Message");
            //  LoginResponse resp = Classes.Responses.getLoginResponseFromResponse(response); // gets account from string

            return View();
        }

        public IActionResult Edit()
        {
            //if (product != null && ApiKey != Guid.Empty && product.Name != null && product.Cost > 0 && product.CategoryId != null)
                // create request to register test
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/Account/Edit?Username=Danny&&Password=Danny123&&AccountType=Resteraunt&&Address=123Testing&&ApiKey={1a07e8f6-825c-442d-a3d7-1315e6780697}") as HttpWebRequest;
            request.Method = "PUT";
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
        public IActionResult Index()
        {

            // create request to register test
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/StoreManagement/AddCategory?category=TestFromCode") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = "StubTest";
            request.Headers.Add("ApiKey", "{1a07e8f6-825c-442d-a3d7-1315e6780697}");
          


            HttpWebResponse response = null;
            //Response from api
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
            }
            string data = Classes.Responses.getKeyFromResponse(response, "Message");
            //  LoginResponse resp = Classes.Responses.getLoginResponseFromResponse(response); // gets account from string

            return View();
        }
    }
}