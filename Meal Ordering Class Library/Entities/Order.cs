using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Meal_Ordering_Class_Library.Entities
{
    public class Order
    {
        //PK
        public int OrderId { get; set; }
        //FK
        public int StoreId { get; set; } = 1;
        //FK
        public string? Username{ get; set; }
        public string? Status {  get; set; }
        //Nav
        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
