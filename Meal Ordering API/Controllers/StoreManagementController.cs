using Meal_Ordering_API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Meal_Ordering_API.Controllers
{
    public class StoreManagementController : Controller
    {
        /// <summary>
        /// Adds a product to the store. Takes the ApiKey and a product
        /// Type : POST
        /// </summary>
        /// <param name="product"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/StoreManagement/Add")]
        public string Add(Product product,[FromHeader] Guid ApiKey)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
        /// <summary>
        /// Removes an item from the store.
        /// Type : POST
        /// </summary>
        /// <param name="product"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/StoreManagement/Remove")]
        public string Remove(Product product, [FromHeader] Guid ApiKey)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
        /// <summary>
        /// Edits a product for the store.
        /// Type: POST
        /// </summary>
        /// <param name="product"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/StoreManagement/Edit")]
        public string Edit(Product product, [FromHeader] Guid ApiKey)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
        /// <summary>
        /// Adds a category from a string to the given store that links to the ApiKey
        /// Type: POST
        /// </summary>
        /// <param name="category"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/StoreManagement/AddCategory")]
        public string AddCategory(string category, [FromHeader] Guid ApiKey)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
        /// <summary>
        /// Removes a category from the store.
        /// Type: POST
        /// </summary>
        /// <param name="category"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/StoreManagement/RemoveCategory")]
        public string RemoveCategory(Category category, [FromHeader] Guid ApiKey)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
        /// <summary>
        /// Edits a category for the given store.
        /// Type: PUT
        /// </summary>
        /// <param name="category"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPut("/API/V1/StoreManagement/EditCategory")]
        public string EditCategory(Category category, [FromHeader] Guid ApiKey)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
        /// <summary>
        /// Gets all categories from the store.
        /// Type : GET
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
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
