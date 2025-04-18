using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.OrderManagement.Queries.GetOrdersByUserIdQuery
{
    public class GetOrderByIdUserIdQueryHandler : IQueryHandler<GetOrdersByUserIdQuery, List<GetOrdersByUserIdDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<GetOrderByIdUserIdQueryHandler> _logger;
        public GetOrderByIdUserIdQueryHandler(IOrderRepository orderRepository,
                                              ILogger<GetOrderByIdUserIdQueryHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<List<GetOrdersByUserIdDto>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _orderRepository.GetByUserIdAsync(request.UserId);

                var orderDto = orders.Select(o => new GetOrdersByUserIdDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    OrderTotal = o.OrderTotal,
                    Status = o.Status.ToString()
                }).ToList();
                return orderDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy đơn hàng theo userId {UserId}", request.UserId);
                throw;
            }
        }
    }
}
