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
            bool check = false;
            string message = "";
            //verify
            if (ApiKey != Guid.Empty && product != null )
            {
                check = true;
                message = "Add item";
            }
            else
            {
                check = false;
                message = "Either Guid is empty or Product is null";
            }


            // Set Headers
            Response.Headers.UserAgent = "API";
            Response.Headers["Message"] = message;

            if (check)
            {
                Response.StatusCode = 200;
            }
            else
            {
                Response.StatusCode = 400;
            }
            //return
            return "";
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
            bool check = false;
            string message = "";
            //verify
            if (ApiKey != Guid.Empty && product != null)
            {
                check = true;
                message = "Remove item";
            }
            else
            {
                check = false;
                message = "Either Guid is empty or Product is null";
            }


            // Set Headers
            Response.Headers.UserAgent = "API";
            Response.Headers["Message"] = message;

            if (check)
            {
                Response.StatusCode = 200;
            }
            else
            {
                Response.StatusCode = 400;
            }
            //return
            return "";
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
            bool check = false;
            string message = "";
            //verify
            if (ApiKey != Guid.Empty && product != null)
            {
                check = true;
                message = "Edit item";
            }
            else
            {
                check = false;
                message = "Either Guid is empty or Product is null";
            }


            // Set Headers
            Response.Headers.UserAgent = "API";
            Response.Headers["Message"] = message;

            if (check)
            {
                Response.StatusCode = 200;
            }
            else
            {
                Response.StatusCode = 400;
            }
            //return
            return "";
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
            bool check = false;
            string message = "";
            //verify
            if (ApiKey != Guid.Empty && category != null)
            {
                check = true;
                message = "Add Category";
            }
            else
            {
                check = false;
                message = "Either Guid is empty or Category is null";
            }


            // Set Headers
            Response.Headers.UserAgent = "API";
            Response.Headers["Message"] = message;

            if (check)
            {
                Response.StatusCode = 200;
            }
            else
            {
                Response.StatusCode = 400;
            }
            //return
            return "";
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
            bool check = false;
            string message = "";
            //verify
            if (ApiKey != Guid.Empty && category != null)
            {
                check = true;
                message = "Remove Category";
            }
            else
            {
                check = false;
                message = "Either Guid is empty or Category is null";
            }


            // Set Headers
            Response.Headers.UserAgent = "API";
            Response.Headers["Message"] = message;

            if (check)
            {
                Response.StatusCode = 200;
            }
            else
            {
                Response.StatusCode = 400;
            }
            //return
            return "";
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
            bool check = false;
            string message = "";
            //verify
            if (ApiKey != Guid.Empty && category != null)
            {
                check = true;
                message = "Edit Category";
            }
            else
            {
                check = false;
                message = "Either Guid is empty or Category is null";
            }


            // Set Headers
            Response.Headers.UserAgent = "API";
            Response.Headers["Message"] = message;

            if (check)
            {
                Response.StatusCode = 200;
            }
            else
            {
                Response.StatusCode = 400;
            }
            //return
            return "";
        }
        /// <summary>
        /// Gets all categories from the store.
        /// Type : GET
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpGet("/API/V1/StoreManagement/getAllCategories")]
        public string GetAllCategories()
        {
            List<Category> categories = new List<Category>();

            Category category1 = new Category()
            {
                Name = "Category1",
                Id = 1
            };
            Category category2 = new Category()
            {
                Name = "Category2",
                Id = 1
            };

            categories.Add(category1);
            categories.Add(category2);
            return JsonSerializer.Serialize(categories);
        }
    }
}
