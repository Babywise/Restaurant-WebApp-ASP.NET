using Meal_Ordering_Class_Library.Entities;

namespace Meal_Ordering_Class_Library.ResponseEntities
{
    public class LoginResponse
    {
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
