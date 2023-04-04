using Azure;
using Meal_Ordering_API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Meal_Ordering_API.Controllers
{
    public class AccountController : Controller
    {
        public string Register()
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            Response.Headers["Message"] = "Registered";
            return null;
          
        }

        public string Login()
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }

        public string Edit()
        {
            Account acc = new Account();
            acc.FirstName = "Danny";
            Response.Headers.UserAgent = "API";
            return JsonSerializer.Serialize(acc);
        }
    }
}
