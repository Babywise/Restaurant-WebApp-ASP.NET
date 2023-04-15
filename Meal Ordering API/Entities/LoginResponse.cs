namespace Meal_Ordering_API.Entities
{
    public class LoginResponse
    {
        public Account? user { get; set; }
        public List<Category>? categories { get; set; }
        public List<Product>? products { get; set; }
        public List<Order>? orders { get; set; }

    }
}
