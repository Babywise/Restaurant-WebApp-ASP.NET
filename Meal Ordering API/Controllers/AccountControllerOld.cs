using Azure;
using Meal_Ordering_Class_Library.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Text.Json;
using Meal_Ordering_API.Classes;
using Meal_Ordering_WebApp.Entities;
using System;
using System.IO;
using System.Diagnostics;
using Microsoft.AspNetCore.Components.Forms;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
/*
namespace Meal_Ordering_API.Controllers
{
    /// <summary>
    /// AccountController is the API interface with all things to do with an account
    /// v1
    ///     - Register
    ///     - Edit
    ///     - Login
    /// </summary>
    public class AccountControllerOld : Controller
    {
        private MealOrderingAPIContext _dbContext;

        /// <summary>
        /// Home controller handles all home related functions with the home page.
        /// </summary>
        /// <param name="dbContext"></param>
        public AccountControllerOld(MealOrderingAPIContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Hashes a string using SHA256
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }


        /// <summary>s
        /// Register an account with the API. Takes in an account object. Must have username, password, address filled out at minimum
        /// Returns 200 OK if good, sets response.headers["Message"] to status message if success or fail for more details
        /// Type : GET
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/Account/Register")]
        public string Register([FromBody] Account account)
        {
            
            bool check = true;
            string message = "";


            // account is not null
            if (account != null)
            {
             
                List<Account> accounts = _dbContext.Account.Where(d => d.Username == account.Username).ToList();
         
                if (accounts.Count>=1)
                {
                    check= false;
                    message = "User already exists";
                }
                // validate address
                if (account.Address.Length <= 4) // -------------------------------Not valid validation... must be changed --------------------------
                {
                    message += " Address is too short";
                    check = false;
                }
                if (account.Address.Length > 20)
                {
                    message += " Address is too long";
                    check = false;
                }


                // account type validation
                switch (account.AccountType)
                {
                    case "Restaurant":
                        break;
                    case "Customer":
                        break;
                    default:
                        check = false;
                        message = "Invalid Account Type";
                        break;
                }

                //Username validation
                if (account.Username.Length <= 4) // username has to be larger then 4
                {
                    message += " Username is too short";
                    check = false;
                }
                if (account.Username.Length > 20)
                {
                    message += " Username is too long";
                    check = false;
                }

                //Password Validation
                if (account.Password.Length <= 4) // username has to be larger then 4
                {
                    message += " Password is too short";
                    check = false;
                }
                if (account.Password.Length > 20)
                {
                    message += " Password is too long";
                    check = false;
                }
                if (account.Password.Any(char.IsUpper) && account.Password.Any(char.IsLower) && account.Password.Any(char.IsDigit)) // check to see if password has at least 1 lower, 1 upper, and 1 digit
                {

                }
                else
                {
                    message += " Password must contain a lower character, upper character, and a digit";
                    check = false;
                }




                //Create account
                if (account != null && check == true)
                {
                    try
                    {
                        Guid key = Guid.NewGuid();
                        account.ApiKey = key;
                        account.Password = hash(account.Password);
                        List<Account> IdFromAccount = _dbContext.Account.OrderByDescending(b => b.Id).ToList();
                        if (IdFromAccount.Count>0)
                        {
                            account.Id = IdFromAccount[0].Id + 1;
                        }
                        else
                        {
                            account.Id = 1;
                        }
                        
                        _dbContext.Add(account);
                        _dbContext.SaveChanges();
                      

                        message = "Account Created";
                    }
                    catch (Exception ex)
                    {
                        TextWriterTraceListener logListener = new TextWriterTraceListener("./Log.txt", "Logs");
                        Trace.Listeners.Add(logListener);
                        Trace.WriteLine(ex.Message);
                        Trace.Close();
                        check = false;
                        message = "500 Internal Error";
                    }
                }
            }
            else // account is null
            {
                message = "Account is null";
                check = false;
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
                Response.StatusCode = 200;
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
        public string Login([FromBody] Account account)
        {
            bool check = true;
            string message = "";

            //verify
            List<Account> accounts = _dbContext.Account.Where(b => b.Username == account.Username).ToList();

            if (account == null || account.Password == null || account.Username == null) // ensure all required fields are filled in
            {
                message = "Account, Password, or Username is null";
                check = false;
            }
            else { // all required fields are filled in
                if (accounts.Count == 0) // if no accounts match username
                {
                    message = "No Account found";
                    check = false;
                }
                else
                {
                    //account exists now verify password
                    if (hash(account.Password) != accounts[0].Password)
                    {
                        message = "Password is invalid";
                        check = false;
                    }
                }
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
                Response.StatusCode = 200;
            }


            //return


            LoginResponse response = new LoginResponse();
            if (check == true)
            {
                response.Account = accounts[0];
                response.Account.Password = "";
                response.Categories = _dbContext.Category.ToList();
                response.Products = _dbContext.Product.ToList();
                switch (accounts[0].AccountType)
                {
                    case "Restaurant": // return all orders that are linked to the restaurant
                        response.Orders = _dbContext.Order.Where(b => b.StoreId == accounts[0].Id).ToList();
                        break;
                    case "Customer": // return all orders related to the customer that requested
                        response.Orders = _dbContext.Order.Where(b => b.CustomerId == accounts[0].Id).ToList();
                        break;
                }
            }
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
        public string Edit([FromBody] Account account, [FromHeader] Guid ApiKey)
        {
            bool check = true;
            string message = "";
            List<Account> accounts = _dbContext.Account.Where(b => b.ApiKey== account.ApiKey).ToList();
            if (accounts.Count == 0) // check account for ApiKey if no results check the ApiKey from header
            {
                accounts = _dbContext.Account.Where(b => b.ApiKey == ApiKey).ToList();
                if (accounts.Count == 0) // if both header and account dont have a valid apikey then set false
                {
                    check = false;
                    message = "Invalid Api-Key";
                }
            }
            else // ApiKey matches
            {


                //Validation
                // validate address
                if (account.Address.Length <= 4) // -------------------------------Not valid validation... must be changed --------------------------
                {
                    message += " Address is too short";
                    check = false;
                }
                if (account.Address.Length > 20)
                {
                    message += " Address is too long";
                    check = false;
                }


                // account type validation
                switch (account.AccountType)
                {
                    case "Restaurant":
                        break;
                    case "Customer":
                        break;
                    default:
                        check = false;
                        message = "Invalid Account Type";
                        break;
                }

                //Username validation
                if (account.Username.Length <= 4) // username has to be larger then 4
                {
                    message += " Username is too short";
                    check = false;
                }
                if (account.Username.Length > 20)
                {
                    message += " Username is too long";
                    check = false;
                }

                //Password Validation
                if (account.Password.Length <= 4) // username has to be larger then 4
                {
                    message += " Password is too short";
                    check = false;
                }
                if (account.Password.Length > 20)
                {
                    message += " Password is too long";
                    check = false;
                }
                if (account.Password.Any(char.IsUpper) && account.Password.Any(char.IsLower) && account.Password.Any(char.IsDigit)) // check to see if password has at least 1 lower, 1 upper, and 1 digit
                {

                }
                else
                {
                    message += " Password must contain a lower character, upper character, and a digit";
                    check = false;
                }







                if (check == true) // account is valid update it
                {
                    account.Id = accounts[0].Id;
                    account.Password = hash(account.Password);
                    account.ApiKey = accounts[0].ApiKey;
                    _dbContext.Entry(accounts[0]).State = EntityState.Detached;
                    try
                    {
                        _dbContext.Update(account);
                        _dbContext.SaveChanges();
                        message = "Updated";
                    }
                    catch (Exception ex)
                    {
                        TextWriterTraceListener logListener = new TextWriterTraceListener("./Log.txt", "Logs");
                        Trace.Listeners.Add(logListener);
                        Trace.WriteLine(ex.Message);
                        Trace.Close();
                        check = false;
                        message = "500 Internal Error";
                    }
                }
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
                Response.StatusCode = 200;
            }


            //return
            return "";

        }
    }
}
*/