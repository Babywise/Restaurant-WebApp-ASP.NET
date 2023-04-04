namespace Meal_Ordering_API.Entities
{
    public class Product
    {
        public string? Name { get; set; }   
        public float? Cost { get; set; }   
        public int? Inventory { get; set; }
        public int? Id { get; set; }
        public int? CategoryId { get; set; }
        public int? StoreId { get; set; }
        public int? Quantity { get; set; }
        public bool? Available { get; set; }
    }
}
