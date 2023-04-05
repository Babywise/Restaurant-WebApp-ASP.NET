using Meal_Ordering_API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Meal_Ordering_API.Controllers
{
    public class OrderingController : Controller
    {
        /// <summary>
        /// Adds an item to cart. Takes an ApiKey from header. Also takes a product.
        /// Type : POST
        /// </summary>
        /// <param name="product"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/Ordering/Add")]
        public string Add(Product product, int Quantity, [FromHeader] Guid ApiKey)
        {
            bool check = false;
            string message = "";
            //verify
            if (ApiKey!= Guid.Empty && product != null && Quantity > 0)
            {
                check = true;
                message = "Add item";
            }
            else
            {
                check = false;
                message = "Either Guid is empty or Product is null or Quantity <= 0";
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
        /// removes an item from the users cart. Requires an ApiKey and Product
        /// Type : PUT
        /// </summary>
        /// <param name="product"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPut("/API/V1/Ordering/Remove")]
        public string Remove(Product product, int Quantity, [FromHeader] Guid ApiKey)
        {
            bool check = false;
            string message = "";
            //verify
            if (ApiKey != Guid.Empty && product != null && Quantity > 0)
            {
                check = true;
                message = "Remove item";
            }
            else
            {
                check = false;
                message = "Either Guid is empty or Product is null or Quantity <= 0";
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
        /// Places an order for the current user based on the ApiKey
        /// Type: POST
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/Ordering/placeOrder")]
        public string Place([FromHeader] Guid ApiKey)
        {
            bool check = false;
            string message = "";
            //verify
            if (ApiKey != Guid.Empty)
            {
                check = true;
                message = "Cart Checked-out";
            }
            else
            {
                check = false;
                message = "Guid is empty";
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
        /// Updates an order. Update order status of an existing order to change
        /// Type: PUT
        /// </summary>
        /// <param name="order"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPut("/API/V1/Ordering/update")]
        public string Update(Order order, [FromHeader] Guid ApiKey)
        {
            bool check = false;
            string message = "";
            //verify
            if (order != null && ApiKey != Guid.Empty)
            {
                check = true;
                message = "Updated Order";
            }
            else
            {
                check = false;
                message = "Either Guid is empty or order is null";
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
        /// Gets all available products from the store
        /// Type : GET
        /// </summary>
        /// <returns></returns>
        [HttpGet("/API/V1/Ordering/getAllProducts")]
        public string GetAllProducts()
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }

        /// <summary>
        /// Get all available products from the category passed in. Returns an empty list on fail
        /// Type : GET
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet("/API/V1/Ordering/GetAllProducts")]
        public string GetAllProductsCategory(Category category)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }

        /// <summary>
        /// Gets all orders from the database for the store or user.
        /// Type : GET
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpGet("/API/V1/Ordering/getAllOrders")]
        public string GetAllOrders([FromHeader] Guid ApiKey)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }

    }
}
