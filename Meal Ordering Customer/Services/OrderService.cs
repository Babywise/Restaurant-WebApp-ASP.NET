using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.Services;

namespace Meal_Ordering_Customer.Services
{
    public class OrderService : BaseOrderService
    {
        public OrderService(IConfiguration config, HttpClient httpClient) : base(config, httpClient)
        {
        }

        public async Task<GetOrdersRequest> GetOrdersByUsernameAsync(string accessToken, string Username)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<GetOrdersRequest>($"api/v2/order/orders?Username={Username}");
        }

    }
}
