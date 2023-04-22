using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.Services;

namespace Meal_Ordering_Customer.Services
{
    public class MenuService : BaseMenuService
    {
        public MenuService(IConfiguration config, HttpClient httpClient) : base(config, httpClient)
        {
        }

        public async Task<Category> GetCategoryByIdAsync(string accessToken, int CategoryId, bool IncludeProduct)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<Category>($"api/v2/menu/category?CategoryId={CategoryId}&IncludeProduct={IncludeProduct}");
        }

        public async Task<Product> GetProductByIdAsync(string accessToken, int ProductId)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<Product>($"api/v2/menu/product?ProductId={ProductId}");
        }

    }
}
