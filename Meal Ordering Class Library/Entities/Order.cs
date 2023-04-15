using System.ComponentModel.DataAnnotations.Schema;

namespace Meal_Ordering_Class_Library.Entities
{
    public class Order
    {
        public int? OrderId { get; set; }
        //FK
        public int? StoreId { get; set; }
        //FK
        public int? CustomerId { get; set; }
        public bool? IsUpdated { get; set; } 
        public string? Status {  get; set; }
        //Nav
        public List<Product>? Products { get; set; }
    }
}
