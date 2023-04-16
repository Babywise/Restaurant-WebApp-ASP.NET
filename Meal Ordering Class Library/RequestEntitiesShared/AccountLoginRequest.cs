using System.ComponentModel.DataAnnotations;

namespace Meal_Ordering_Class_Library.RequestEntitiesShared
{
    public class AccountLoginRequest
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
