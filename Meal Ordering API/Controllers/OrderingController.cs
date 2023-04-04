using Meal_Ordering_API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Meal_Ordering_API.Controllers
{
    public class OrderingController : Controller
    {
        [HttpPost("/API/V1/Ordering/Add")]
        public string Add(Product product)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }

        [HttpPut("/API/V1/Ordering/Remove")]
        public string Remove(Product product)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }

        [HttpPost("/API/V1/Ordering/placeOrder")]
        public string Place()
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }

        [HttpPut("/API/V1/Ordering/update")]
        public string Update(Order order)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }

        [HttpGet("/API/V1/Ordering/getAllProducts")]
        public string GetAllProducts()
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }

        [HttpGet("/API/V1/Ordering/GetAllProducts")]
        public string GetAllProductsCategory(Category category)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }

        [HttpGet("/API/V1/Ordering/getAllOrders")]
        public string GetAllOrders()
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }

    }
}
