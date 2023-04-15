using System.ComponentModel.DataAnnotations.Schema;

namespace Meal_Ordering_Class_Library.Entities
{
    public class Product
    {
        public int? OrderId { get; set; }
        public string? Name { get; set; }
        [Column(TypeName = "float")]
        public float? Cost { get; set; }   
        public int? Inventory { get; set; }
        public int? Id { get; set; }
        public int? CategoryId { get; set; }
        public int? StoreId { get; set; }
        public int? Quantity { get; set; }
        [Column(TypeName = "tinyint")]
        public bool? Available { get; set; }
        [Column(TypeName = "tinyint")]
        public bool? Status { get; set; }
        public int? CustomerId { get; set; }    
    }
}
