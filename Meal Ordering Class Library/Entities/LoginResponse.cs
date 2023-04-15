namespace Meal_Ordering_Class_Library.Entities
{
    public class LoginResponse
    {
        public Account? Account { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Product>? Products { get; set; }
        public List<Order>? Orders { get; set; }

    }
}
