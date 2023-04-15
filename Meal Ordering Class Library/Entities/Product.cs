using System.ComponentModel.DataAnnotations.Schema;

namespace Meal_Ordering_Class_Library.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public float? Cost { get; set; } 
        //FK
        public int? StoreId { get; set; }
        //FK
        public int CategoryId { get; set; }
        //Nav
        public Category? Category { get; set; }
    }
}
