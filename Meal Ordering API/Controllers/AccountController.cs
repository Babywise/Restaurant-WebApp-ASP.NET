using Azure;
using Meal_Ordering_API.Entities;
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


            return JsonSerializer.Serialize(acc);
        }
    }
}
