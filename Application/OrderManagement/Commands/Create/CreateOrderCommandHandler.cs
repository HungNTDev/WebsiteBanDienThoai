using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using Domain.Entities;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.OrderManagement.Commands.Create
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, ApiResponse<object>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        public CreateOrderCommandHandler(IOrderRepository orderRepository,
                                         IUnitOfWork unitOfWork,
                                         ILogger<CreateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<ApiResponse<object>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    UserId = request.model.UserId,
                    OrderDate = DateTime.UtcNow,
                    OrderTotal = request.model.OrderTotal,
                    Status = OrderStatus.Pending,
                    OrderItems = request.model.Items.Select(i => new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductItemId = i.ProductItemId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                    }).ToList(),
                };

                await _orderRepository.CreateAsync(order);
                await _unitOfWork.SaveChangesAsync();

                return ApiResponseBuilder.Success<object>(order, "Tạo đơn hàng thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Create Product Item");
                return ApiResponseBuilder.Error<object>("An unexpected error occurred", statusCode: 500);
            }
        }
    }
}
