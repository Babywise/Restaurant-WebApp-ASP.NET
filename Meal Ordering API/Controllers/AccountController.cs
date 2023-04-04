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
    public class AccountController : Controller
    {
        [HttpGet("/API/V1/Account/Register")]
        public string Register(Account account)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            Response.Headers["Message"] = "Registered";
            return "hi";
          
        }
        [HttpPost("/API/V1/Account/Login")]
        public string Login(Account account)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
        [HttpPut("/API/V1/Account/Edit")]
        public string Edit(Account account)
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
    }
}
