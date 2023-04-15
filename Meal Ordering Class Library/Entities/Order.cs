using System.ComponentModel.DataAnnotations.Schema;

namespace Meal_Ordering_Class_Library.Entities
{
    public class Order
    {
 
        public List<Product>? products { get; set; }
        public int? StoreId { get; set; }
        public int? CustomerId { get; set; }
        public int? Id { get; set; }
        [Column(TypeName = "tinyint")]
        public bool? Updated { get; set; } 
        public string? Status {  get; set; }
    }
}
