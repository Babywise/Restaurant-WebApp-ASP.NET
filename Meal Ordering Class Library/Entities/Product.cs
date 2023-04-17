using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Meal_Ordering_Class_Library.Entities
{
    public class Product
    {
        //PK
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public float? Cost { get; set; } 
        public bool? IsDeleted { get; set; } = false;
        //FK
        public int? StoreId { get; set; } = 1;
        //FK
        public int CategoryId { get; set; }
        //Nav
        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
