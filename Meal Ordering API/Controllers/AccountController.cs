using Azure;
using Meal_Ordering_API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Text.Json;

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
        public string Register(Accountt account)
        {
            Accountt acc = new Accountt();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            Response.Headers["Message"] = "Registered";
            return "hi";
          
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
        public string Login(Accountt account)
        {
            Accountt acc = new Accountt();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
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
        public string Edit(Accountt account, [FromHeader] Guid ApiKey)
        {
            Accountt acc = new Accountt();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
    }
}
