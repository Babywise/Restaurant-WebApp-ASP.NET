using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesRestaurant;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.ResponseEntities;
using Meal_Ordering_Class_Library.Services;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Meal_Ordering_Restaurant.Services
{
    public class ManagementService : BaseManagementService
    {
        public ManagementService(IConfiguration config, HttpClient httpClient) : base(config, httpClient)
        {
        }

        public async Task<HttpResponseMessage> AddCategoryAsync(CategoryRequest categoryRequest, string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.PostAsJsonAsync("api/v1/management/add-category", categoryRequest);
        }

        public async Task<HttpResponseMessage> EditCategoryAsync(CategoryRequest categoryRequest, string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.PutAsJsonAsync("api/v1/management/edit-category", categoryRequest);
        }

        public async Task<HttpResponseMessage> DeleteCategoryAsync(CategoryRequest categoryRequest, string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.PostAsJsonAsync($"api/v1/management/delete-category/", categoryRequest);
        }

        public async Task<HttpResponseMessage> AddProductAsync(ProductRequest productRequest, string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.PostAsJsonAsync("api/v1/management/add-product", productRequest);
        }

        public async Task<HttpResponseMessage> EditProductAsync(ProductRequest productRequest, string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.PutAsJsonAsync("api/v1/management/edit-product", productRequest);
        }

        public async Task<HttpResponseMessage> DeleteProductAsync(ProductRequest productRequest, string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.PostAsJsonAsync($"api/v1/management/delete-product/", productRequest);
        }

    }
}
