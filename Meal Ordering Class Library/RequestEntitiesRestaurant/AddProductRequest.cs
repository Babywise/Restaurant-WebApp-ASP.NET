using Meal_Ordering_Class_Library.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Meal_Ordering_Class_Library.RequestEntitiesRestaurant
{
    public class AddProductRequest
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int? Quantity { get; set; }
        [Required]
        public float? Cost { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
