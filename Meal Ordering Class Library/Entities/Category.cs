namespace Meal_Ordering_Class_Library.Entities
{
    public class Category
    {
        //PK
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public bool? IsDeleted { get; set; } = false;
        //Nav
        public ICollection<Product>? Products { get; set; }
    }
}
