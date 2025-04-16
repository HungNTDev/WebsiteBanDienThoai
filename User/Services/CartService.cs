using static User.Pages.ProductDetail;
using System.Net.Http.Json;
using User.Models.Cart;

namespace User.Services
{
    public interface ICartService
    {
        Task<ApiResponse<object>> AddToCartAsync(AddToCartRequest request);
    }

    public class CartService : ICartService
    {
        private readonly HttpClient _http;

        public CartService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ApiResponse<object>> AddToCartAsync(AddToCartRequest request)
        {
            var userId = await GetCurrentUserId(); // giả sử bạn có token chứa userId
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

            var response = await _http.PostAsJsonAsync("api/Cart/create", createCart);

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

        private async Task<Guid> GetCurrentUserId()
        {
            // Giả sử bạn có lưu JWT trong localStorage và decode ra userId
            // Hoặc thay thế bằng cách gọi AuthService nếu có
            return Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"); // hardcode cho test
        }
    }

}
