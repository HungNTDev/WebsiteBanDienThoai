using static User.Pages.ProductDetail;
using System.Net.Http.Json;
using User.Models.Cart;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using static User.Pages.GioHang;

namespace User.Services
{
    public interface ICartService
    {
        Task<ApiResponse<object>> AddToCartAsync(AddToCartRequest request);
        Task<ApiResponse<GetCartByIdDto>> GetCartAsync();
        Task<ApiResponse<object>> UpdateCartAsync(UpdateCartRequest request);
        Task<ApiResponse<object>> RemoveItemAsync(DeleteCartItemRequest request);
        Task<ApiResponse<object>> ClearCartAsync(Guid userId);
    }

    public class CartService : ICartService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        public CartService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        public async Task<ApiResponse<GetCartByIdDto>> GetCartAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrWhiteSpace(token))
            {
                return new ApiResponse<GetCartByIdDto>
                { IsSuccess = false, Message = "Chưa đăng nhập." };
            }

            var userId = GetCurrentUserIdFromToken(token);
            if (userId == Guid.Empty)
            {
                return new ApiResponse<GetCartByIdDto>
                { IsSuccess = false, Message = "Không xác định được người dùng." };
            }

            // Gọi API theo userId thay vì cartId
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Cart/my?userId={userId}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<GetCartByIdDto>
                { IsSuccess = false, Message = "Không thể tải giỏ hàng." };
            }

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<GetCartByIdDto>>();
            return result ?? new ApiResponse<GetCartByIdDto>
            { IsSuccess = false, Message = "Lỗi không xác định." };
        }

        public async Task<ApiResponse<object>> AddToCartAsync(AddToCartRequest request)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrWhiteSpace(token))
            {
                return new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Người dùng chưa đăng nhập."
                };
            }

            var userId = GetCurrentUserIdFromToken(token); // Custom function để lấy userId từ token JWT
            if (userId == Guid.Empty)
            {
                return new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Không xác định được người dùng."
                };
            }

            var createCart = new CreateCartRequest
            {
                UserId = userId,
                CartItems = new List<CartItemDto>
        {
            new CartItemDto
            {
                ProductId = request.ProductItemId,
                Quantity = request.Quantity
            }
        }
            };

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/Cart/create")
            {
                Content = JsonContent.Create(createCart)
            };
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.SendAsync(httpRequest);

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Không thể thêm vào giỏ hàng."
                };
            }

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return result ?? new ApiResponse<object>
            {
                IsSuccess = false,
                Message = "Lỗi không xác định."
            };
        }

        public async Task<ApiResponse<object>> UpdateCartAsync(UpdateCartRequest request)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrWhiteSpace(token))
            {
                return new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Chưa đăng nhập."
                };
            }

            var httpRequest = new HttpRequestMessage(HttpMethod.Put, "api/Cart/update")
            {
                Content = JsonContent.Create(request)
            };

            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.SendAsync(httpRequest);
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Không thể cập nhật giỏ hàng."
                };
            }

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return result ?? new ApiResponse<object> { IsSuccess = false, Message = "Lỗi không xác định." };
        }
        public async Task<ApiResponse<object>> RemoveItemAsync(DeleteCartItemRequest request)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrWhiteSpace(token))
            {
                return new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Chưa đăng nhập."
                };
            }

            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, "api/Cart/delete-item")
            {
                Content = JsonContent.Create(request)
            };
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.SendAsync(httpRequest);
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Không thể xóa sản phẩm khỏi giỏ hàng."
                };
            }

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return result ?? new ApiResponse<object> { IsSuccess = false, Message = "Lỗi không xác định." };
        }

        public async Task<ApiResponse<object>> ClearCartAsync(Guid userId)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrWhiteSpace(token))
            {
                return new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Chưa đăng nhập."
                };
            }

            var request = new HttpRequestMessage(HttpMethod.Delete, "api/Cart/clear")
            {
                Content = JsonContent.Create(new { userId })
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Không thể xóa giỏ hàng."
                };
            }

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return result ?? new ApiResponse<object> { IsSuccess = false, Message = "Lỗi không xác định." };
        }


        private Guid GetCurrentUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var userIdClaim = jwt.Claims.FirstOrDefault(x => x.Type == "Id");
            return Guid.TryParse(userIdClaim?.Value, out var userId) ? userId : Guid.Empty;
        }
    }
}
