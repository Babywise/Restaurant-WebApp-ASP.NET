using Meal_Ordering_Class_Library.Entities;

namespace Meal_Ordering_Restaurant.RequestEntities
{
    public class AccountRegisterRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AccountType { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
