using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Meal_Ordering_Class_Library.Entities
{
    public class Product
    {
        //PK
        public int ProductId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int? Quantity { get; set; }
        [Required]
        public float? Cost { get; set; } 
        public bool? IsDeleted { get; set; } = false;
        //FK
        public int? StoreId { get; set; } = 1;
        //FK
        [Required(ErrorMessage = "The Category field is required")]
        public int? CategoryId { get; set; }
        //Nav
        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
