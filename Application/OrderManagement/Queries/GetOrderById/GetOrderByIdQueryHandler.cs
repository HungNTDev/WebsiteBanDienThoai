using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.OrderManagement.Commands.Create;
using Microsoft.Extensions.Logging;

namespace Application.OrderManagement.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, ApiResponse<GetOrderByIdDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<GetOrderByIdQueryHandler> _logger;
        public GetOrderByIdQueryHandler(IOrderRepository orderRepository,
                                        ILogger<GetOrderByIdQueryHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<ApiResponse<GetOrderByIdDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(request.Id);
                if (order == null)
                {
                    return ApiResponseBuilder.Error<GetOrderByIdDto>("Order not found", statusCode: 404);
                }
                var orderDto = new GetOrderByIdDto
                {
                    Id = order.Id,
                    Code = order.Code,
                    OrderDate = order.OrderDate,
                    OrderTotal = order.OrderTotal,
                    Status = order.Status.ToString(),
                    OrderItems = order.OrderItems.Select(i => new OrderItemDto
                    {
                        ProductItemId = i.ProductItemId,
                        ProductName = i.ProductItem.Name,
                        ProductImage = i.ProductItem.Image,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList(),
                    Payments = order.Payments?.Select(p => new PaymentDto
                    {
                        Provider = p.Provider,
                    }).ToList() ?? new List<PaymentDto>()
                };
                return ApiResponseBuilder.Success(orderDto, "Get order by id successfully");
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }
    }
}
