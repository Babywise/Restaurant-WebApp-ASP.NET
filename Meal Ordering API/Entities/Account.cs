using System.ComponentModel.DataAnnotations.Schema;

namespace Meal_Ordering_API.Entities
{
    public class Account
    {
        public int? Id { get; set; }
        public Guid? ApiKey { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? AccountType { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

    }

}
