using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Application.Abstract.Services
{
    public interface IPayPalService
    {
        Task<string?> CreateOrderAsync(decimal amount, string returnUrl, string cancelUrl);
        Task<bool> CaptureOrderAsync(string orderId);
    }

    public class PayPalService : IPayPalService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public PayPalService(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            var clientId = _config["PayPal:ClientId"];
            var secret = _config["PayPal:Secret"];
            var basicAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{secret}"));

            var request = new HttpRequestMessage
                (HttpMethod.Post, "https://api-m.sandbox.paypal.com/v1/oauth2/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", basicAuth);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8,
                "application/x-www-form-urlencoded");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<JsonElement>(json);
            return data.GetProperty("access_token").GetString()!;
        }

        public async Task<string?> CreateOrderAsync(decimal amount, string returnUrl, string cancelUrl)
        {
            var accessToken = await GetAccessTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var payload = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
                new
                {
                    amount = new
                    {
                        currency_code = "USD",
                        value = amount.ToString("F2")
                    }
                }
            },
                application_context = new
                {
                    return_url = returnUrl,
                    cancel_url = cancelUrl
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post,
                "https://api-m.sandbox.paypal.com/v2/checkout/orders")
            {
                Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            var root = JsonSerializer.Deserialize<JsonElement>(json);
            var links = root.GetProperty("links").EnumerateArray();

            foreach (var link in links)
            {
                if (link.GetProperty("rel").GetString() == "approve")
                {
                    return link.GetProperty("href").GetString();
                }
            }
            return null;
        }

        public async Task<bool> CaptureOrderAsync(string orderId)
        {
            var accessToken = await GetAccessTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var request = new HttpRequestMessage(HttpMethod.Post,
                $"https://api-m.sandbox.paypal.com/v2/checkout/orders/{orderId}/capture");

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}
