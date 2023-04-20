using System.ComponentModel.DataAnnotations.Schema;

namespace Meal_Ordering_Class_Library.Entities
{
    public class Account
    {
        //login & edit & register (all)
        public string? UserName { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmNewPassword { get; set; }

        //edit & register
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        //Register only
        public string? AccountType { get; set; }
    }
}
