using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.OrderManagement.Queries.GetOrderHistory
{
    public class GetOrderHistoryQueryHandler
        : IQueryHandler<GetOrderHistoryQuery, ApiResponse<List<OrderHistoryDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<GetOrderHistoryQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetOrderHistoryQueryHandler(
                                           ILogger<GetOrderHistoryQueryHandler> logger,
                                           IHttpContextAccessor httpContextAccessor,
                                           IOrderRepository orderRepository)
        {

            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _orderRepository = orderRepository;
        }

        public async Task<ApiResponse<List<OrderHistoryDto>>>
            Handle(GetOrderHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return ApiResponseBuilder.Error<List<OrderHistoryDto>>("Không xác định được người dùng");
                }

                var orders = await _orderRepository.GetOrdersByUserIdAsync(Guid.Parse(userId));

                //var orders = await _orderRepostirory.GetOrdersByUserIdAsync(request.UserId);

                var result = orders.Select(o => new OrderHistoryDto
                {
                    Id = o.Id,
                    Code = o.Code,
                    OrderDate = o.OrderDate,
                    Total = o.OrderTotal,
                    Status = o.Status.ToString(),
                    Items = o.OrderItems.Select(i => new OrderItemHistoryDto
                    {
                        ProductName = i.ProductItem?.Product?.Name ?? "Không xác định",
                        ProductImage = i.ProductItem?.Image ?? "",
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                }).ToList();

                return ApiResponseBuilder.Success(result, "Hiển thị thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy đơn hàng theo người dùng");
                throw;
            }
        }
    }
}
