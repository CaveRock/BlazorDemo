using BlazorDemo.API.Routes;
using BlazorDemo.Core.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorDemo.UI.BlazorWASM.Code {
    public class AuthenticationManager {
        
        private readonly HttpClient _httpClient;
        public AuthenticationManager(
            IHttpClientFactory httpClientFactory
            )
        {
            _httpClient = httpClientFactory.CreateClient("api"); 
        }
        public async Task<ServiceActionResult<RegisterUserModel>> Register(RegisterUserModel user)
        {
            var serviceResult = new ServiceActionResult<RegisterUserModel>(user);

            var httpResult = await _httpClient.PostAsJsonAsync<RegisterUserModel>(ApiEndpoints.Authentication.Register, user);

            if (httpResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await httpResult.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ServiceActionResult<RegisterUserModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            } else
            {
                serviceResult.ValidationResult.AddEntityError("An API error occurred");
            }

            return serviceResult;
        }
    }
}
