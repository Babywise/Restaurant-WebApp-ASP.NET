using Meal_Ordering_WebApp.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meal_Ordering_API.Entities
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
