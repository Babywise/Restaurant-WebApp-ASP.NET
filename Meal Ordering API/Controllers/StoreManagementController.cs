using Meal_Ordering_API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Meal_Ordering_API.Controllers
{
    public class StoreManagementController : Controller
    {
        [HttpPost("/API/V1/StoreManagement/Add")]
        public string Add(Product product,[FromHeader] Guid ApiKey)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
        [HttpPost("/API/V1/StoreManagement/Remove")]
        public string Remove(Product product, [FromHeader] Guid ApiKey)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
        [HttpPost("/API/V1/StoreManagement/Edit")]
        public string Edit(Product product, [FromHeader] Guid ApiKey)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
        [HttpPost("/API/V1/StoreManagement/AddCategory")]
        public string AddCategory(string category, [FromHeader] Guid ApiKey)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
        [HttpPost("/API/V1/StoreManagement/RemoveCategory")]
        public string RemoveCategory(Category category, [FromHeader] Guid ApiKey)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
        [HttpPut("/API/V1/StoreManagement/EditCategory")]
        public string EditCategory(Category category, [FromHeader] Guid ApiKey)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
        [HttpGet("/API/V1/StoreManagement/getAllCategories")]
        public string GetAllCategories([FromHeader] Guid ApiKey)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
    }
}
