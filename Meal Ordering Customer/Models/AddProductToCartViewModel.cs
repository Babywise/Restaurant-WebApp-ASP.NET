using Meal_Ordering_Class_Library.Entities;
using System.ComponentModel.DataAnnotations;

namespace Meal_Ordering_Customer.Models
{
    public class AddProductToCartViewModel
    {
        public Product? Product { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be a value one or greater.")]
        public int? QuantityToAdd { get; set; }
    }
}
