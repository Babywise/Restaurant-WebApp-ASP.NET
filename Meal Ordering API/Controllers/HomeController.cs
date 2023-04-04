using Azure;
using Meal_Ordering_API.Entities;
using Meal_Ordering_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

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
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/Account/Login?Username='Danny'") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/text";
            request.UserAgent = "StubTest";
            //Response from api
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Accountt acc = getAccountFromResponse(response); // gets account from string
        
            return View();
        }


        /// <summary>
        /// Converts a string Json into a Account object
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        public Accountt getAccountFromResponse(HttpWebResponse resp)
        {
            Stream responseStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string result = reader.ReadToEnd();
   
            Accountt acc = JsonSerializer.Deserialize<Accountt>(result);
            return acc;
        }
        /// <summary>
        /// Pulls the specififed key from the response provided.. IE : getKeyFromResponse(Response, "User-Agent")
        /// </summary>
        /// <param name="response"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string getKeyFromResponse(HttpWebResponse response,string Key)
        {
            string returnVal = response.Headers[Key]; // set ApiKey for easy reading
            return returnVal;
        }

    }
}