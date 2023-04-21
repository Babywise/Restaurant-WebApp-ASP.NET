using Microsoft.AspNetCore.Identity;

namespace Meal_Ordering_Class_Library.Entities
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AccountType { get; set; }
        public string? Address { get; set; }
    }
}
