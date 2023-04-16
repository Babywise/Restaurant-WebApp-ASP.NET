using Meal_Ordering_Class_Library.ResponseEntitiesShared;
using Meal_Ordering_Restaurant.RequestEntities;
using Newtonsoft.Json;
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
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return await _httpClient.PutAsJsonAsync("api/v1/account/edit", accountEditRequest);
        }

        /*public async Task<T> GetAsync<T>(string requestUri)
        {
            SetAccessTokenHeader();
            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }*/
        /*private void SetAccessTokenHeader()
        {
            if (!string.IsNullOrEmpty(_accessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            }
        }*/

    }
}
