namespace Meal_Ordering_Class_Library.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        //Nav
        public ICollection<Product>? Products { get; set; }
    }
}
