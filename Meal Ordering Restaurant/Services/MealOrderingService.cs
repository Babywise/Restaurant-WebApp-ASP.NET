using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesShared;
using Meal_Ordering_Class_Library.ResponseEntities;
using System.Net.Http.Headers;

namespace Meal_Ordering_Restaurant.Services
{
    public class MealOrderingService
    {
        private readonly HttpClient _httpClient;
        
        public MealOrderingService(string apiAddress)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(apiAddress) };
        }

        public async Task<HttpResponseMessage> LoginAsync(AccountLoginRequest accountLoginRequest)
        {
            return await _httpClient.PostAsJsonAsync("api/v1/account/login", accountLoginRequest);
        }

        public async Task<HttpResponseMessage> RegisterAsync(AccountRegisterRequest accountRegisterRequest)
        {
            return await _httpClient.PostAsJsonAsync("api/v1/account/register", accountRegisterRequest);
        }

        public async Task<HttpResponseMessage> UpdateUserDetailsAsync(AccountEditRequest accountEditRequest, string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            return await _httpClient.PutAsJsonAsync("api/v1/account/edit", accountEditRequest);
        }

        public async Task<AccountEditRequest> GetAccountDetails(string userId, string accessToken)
        {
            SetAccessToken(_httpClient, accessToken);
            _httpClient.DefaultRequestHeaders.Add("UserId", userId);

            return await _httpClient.GetFromJsonAsync<AccountEditRequest>("api/v1/account/edit");
        }
        private void SetAccessToken(HttpClient httpClient, string accessToken)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

    }
}
