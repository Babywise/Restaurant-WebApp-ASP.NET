using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.Services;

namespace Meal_Ordering_Restaurant.Services
{
    public class OrderService : BaseOrderService
    {
        public OrderService(IConfiguration config, HttpClient httpClient) : base(config, httpClient)
        {
        }

        public async Task<GetOrdersRequest> GetOrdersAsync(string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.GetFromJsonAsync<GetOrdersRequest>("api/v2/order/all-orders");
        }
    }
}
