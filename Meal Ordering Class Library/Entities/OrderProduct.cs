using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Meal_Ordering_Class_Library.Entities
{
    public class OrderProduct
    {
        //PK
        public int OrderProductId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        //FK
        public int OrderId { get; set; }
        //Nav
        [JsonIgnore]
        public Order? Order { get; set; }
    }
}
