using Meal_Ordering_Class_Library.Entities;
using System.ComponentModel.DataAnnotations;

namespace Meal_Ordering_Class_Library.RequestEntitiesShared
{
    public class AccountRequest
    {
        //login & edit & register (all)
        public string? UserName { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        [Compare(nameof(NewPassword), ErrorMessage = "NewPassword must match ConfirmNewPassword")]
        public string? ConfirmNewPassword { get; set; }

        //edit & register
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Address { get; set; }
        //Register only
        public string? AccountType { get; set; }
    }
}
