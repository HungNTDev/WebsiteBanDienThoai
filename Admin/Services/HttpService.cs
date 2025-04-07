using Admin.Models;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Admin.Services
{
    public interface IHttpService
    {
        Task<ApiResponse<T>> PostAsync<T>(string url, MultipartFormDataContent content);
        Task<ApiResponse<T>> GetAsync<T>(string url);
        Task<ApiResponse<T>> PutAsync<T>(string url, object data);
        Task<ApiResponse<T>> DeleteAsync<T>(string url);
        Task<T> GetFromJsonAsync<T>(string url);
    }

    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public HttpService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        private async Task AddAuthorizationHeader()
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        private async Task<ApiResponse<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API Response Content: {content}");
            
            if (string.IsNullOrEmpty(content))
            {
                return new ApiResponse<T>
                {
                    IsSuccess = response.IsSuccessStatusCode,
                    Message = response.IsSuccessStatusCode ? "Success" : "Error occurred",
                    StatusCode = (int)response.StatusCode
                };
            }

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Console.WriteLine($"Attempting to parse JSON with options: {JsonSerializer.Serialize(options)}");
                var result = JsonSerializer.Deserialize<ApiResponse<T>>(content, options);
                return result;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Parse Error: {ex.Message}");
                return new ApiResponse<T>
                {
                    IsSuccess = false,
                    Message = $"Error parsing response: {ex.Message}",
                    StatusCode = (int)response.StatusCode
                };
            }
        }

        public async Task<T> GetFromJsonAsync<T>(string url)
        {
            await AddAuthorizationHeader();
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Raw API Response: {content}"); // Log raw response
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<ApiResponse<T>> PostAsync<T>(string url, MultipartFormDataContent content)
        {
            await AddAuthorizationHeader();
            var response = await _httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<T>>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string url)
        {
            await AddAuthorizationHeader();
            var response = await _httpClient.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<T>>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<ApiResponse<T>> PutAsync<T>(string url, object data)
        {
            await AddAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync(url, data);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<T>>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<ApiResponse<T>> DeleteAsync<T>(string url)
        {
            await AddAuthorizationHeader();
            var response = await _httpClient.DeleteAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<T>>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}