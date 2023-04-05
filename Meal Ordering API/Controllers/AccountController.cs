using Azure;
using Meal_Ordering_API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Text.Json;
using Meal_Ordering_API.Classes;
namespace Meal_Ordering_API.Controllers
{
    /// <summary>
    /// AccountController is the API interface with all things to do with an account
    /// v1
    ///     - Register
    ///     - Edit
    ///     - Login
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Register an account with the API. Takes in an account object. Must have username, password, address filled out at minimum
        /// Returns 200 OK if good, sets response.headers["Message"] to status message if success or fail for more details
        /// Type : GET
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpGet("/API/V1/Account/Register")]
        public string Register(Account account)
        {
            bool check = false;
            string message = "";
            //verify
            if (account.Username!= null && account.Password!=null &&account.AccountType!=null) {
                check = true;
                message = "Registered";
            }
            else
            {
                check = false;
                message = "Either Username, Password, or Account type is null";
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
        /// Login to an account. Must have Username and Password at minimum.
        /// Returns a Response.headers["ApiKey"] if success for the account logged into. 
        /// Else returns 400 
        ///     With Response.headers["Message"] with a detailed message about what went wrong
        /// Type : POST
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/Account/Login")]
        public string Login(Account account)
        {
            bool check = false;
            string message = "";
            //verify
            if (account.Username != null && account.Password != null)
            {
                check = true;
                message = "Login";
            }
            else
            {
                check = false;
                message = "Either Username, or Password is null";
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
            Category cat = new Category();
            cat.Name = "STUB";

            Product prod = new Product();
            prod.Cost = 1;
            prod.Name = "Banana";
            
            Order order= new Order();
            order.Status = "Cart";
            order.Id = 1;
            order.StoreId = 1;
            order.CustomerId = 1;   
            order.products = new List<Product>();
            order.products.Add(prod);

            LoginResponse response = new LoginResponse();
            response.products = new List<Product>();   
            response.products.Add(prod);
            response.orders = new List<Order>();    
            response.orders.Add(order); 
            response.user = account;
            response.categories = new List<Category>();
            response.categories.Add(cat);

            return JsonSerializer.Serialize(response);

        }

        /// <summary>
        /// Edit the fields in an account. Fields left null or "" will be ignored. Only values with data will be compared and changed.
        /// Returns 200 OK on success
        ///     Response.headers["Message"] for a message about the sucess or failure
        /// Returns 400 Bad Request on failure
        ///     Response.headers["Message"] for a message about the sucess or failure
        /// Type : PUT
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPut("/API/V1/Account/Edit")]
        public string Edit(Account account, [FromHeader] Guid ApiKey)
        {
            bool check = false;
            string message = "";
            //verify
            if (account.Username != null && account.Password != null && account.AccountType != null)
            {
                check = true;
                message = "Edited";
            }
            else
            {
                check = false;
                message = "Either Username, Password, or Account type is null";
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
    }
}
