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
        public IActionResult addCategory()
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

        public IActionResult removeProduct()
        {

            // create request to register test
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/StoreManagement/remove") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = "StubTest";
            request.Headers.Add("ApiKey", "{1a07e8f6-825c-442d-a3d7-1315e6780697}");

            Product product = new Product()
            {
                Cost = 1,
                Name = "Test",
                CategoryId = 0,
                Id= 0
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


        public IActionResult removeCategory()
        {

            // create request to register test
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/StoreManagement/RemoveCategory") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = "StubTest";
            request.Headers.Add("ApiKey", "{1a07e8f6-825c-442d-a3d7-1315e6780697}");

            Category category = new Category()
            {
               Name= "TestFromCode"
            };
            string stringData = JsonSerializer.Serialize(category); // place body here
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
        public IActionResult editCategory()
        {

            // create request to register test
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/StoreManagement/EditCategory") as HttpWebRequest;
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.UserAgent = "StubTest";
            request.Headers.Add("ApiKey", "{1a07e8f6-825c-442d-a3d7-1315e6780697}");

            Category category = new Category()
            {
                Name = "Testing2",
                Id = 0
            };
            string stringData = JsonSerializer.Serialize(category); // place body here
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

        public IActionResult editProduct()
        {

            // create request to register test
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/StoreManagement/Edit") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = "StubTest";
            request.Headers.Add("ApiKey", "{6e6aa943-69e7-4a99-b464-db7fd04c4e71}");
            Product product = new Product()
            {
                Id = 1,
                Cost = (float?)2.13,
                Name = "Testing2",
                StoreId = 1,
                CategoryId = 1,
                status=false
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
        public IActionResult getAllOrders()
        {

            // create request to register test
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/Ordering/getAllOrders") as HttpWebRequest;
            request.Method = "GET";
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
        public IActionResult getAllProducts()
        {

            // create request to register test
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/Ordering/getAllProducts") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = "StubTest";
            request.Headers.Add("ApiKey", "{1a07e8f6-825c-442d-a3d7-1315e6780697}");
            Category category = new Category()
            {
                Id = 0,
                Name = "Blah"
            };
            string stringData = JsonSerializer.Serialize(category); // place body here
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
        public IActionResult getAllProductsFromCategory()
        {

            // create request to register test
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/Ordering/getAllProducts") as HttpWebRequest;
            request.Method = "GET";
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
        public IActionResult updateStatus()
        {

            // create request to register test
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/Ordering/update") as HttpWebRequest;
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.UserAgent = "StubTest";
            request.Headers.Add("ApiKey", "{1a07e8f6-825c-442d-a3d7-1315e6780697}");

            Order order = new Order()
            {
                Id = 0,
                CustomerId=1,
                StoreId=3,
                Updated=false,
                Status="Started"
            };
            string stringData = JsonSerializer.Serialize(order); // place body here
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
        public IActionResult Register()
        {

            // create request to register test
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/Account/Register") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = "StubTest";
            request.Headers.Add("ApiKey", "{1a07e8f6-825c-442d-a3d7-1315e6780697}");

            Account account = new Account()
            {
                Address = "1234Testing",
                Username = "Danny",
                Password = "Danny12",
                AccountType = "Customer"
            };
            string stringData = JsonSerializer.Serialize(account); // place body here
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
        public IActionResult placeOrder()
        {

            // create request to register test
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/Ordering/placeOrder") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = "StubTest";
            request.Headers.Add("ApiKey", "{2bcf9c86-dad9-4fee-a884-769dd60f4b0f}");


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
        public IActionResult add()
        {

            // create request to register test
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/Ordering/Add?storeId=1&&itemId=1&&Quantity=2") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = "StubTest";
            request.Headers.Add("ApiKey", "{2bcf9c86-dad9-4fee-a884-769dd60f4b0f}");


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
        public IActionResult Index()
        {

            // create request to register test
            HttpWebRequest request = WebRequest.Create("https://localhost:7062/API/V1/Ordering/Remove?storeId=1&&itemId=4") as HttpWebRequest;
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.UserAgent = "StubTest";
            request.Headers.Add("ApiKey", "{2bcf9c86-dad9-4fee-a884-769dd60f4b0f}");


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