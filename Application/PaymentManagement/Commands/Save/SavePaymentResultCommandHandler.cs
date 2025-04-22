using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using Domain.Entities;
using Domain.Enum;
using Microsoft.Extensions.Logging;

namespace Application.PaymentManagement.Commands.Save
{
    public class SavePaymentResultCommandHandler : ICommandHandler<SavePaymentResultCommand, ApiResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SavePaymentResultCommandHandler> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly IUserPaymentRepository _userPaymentRepository;

        public SavePaymentResultCommandHandler(IUnitOfWork unitOfWork,
                                               ILogger<SavePaymentResultCommandHandler> logger,
                                               IOrderRepository orderRepository,
                                               IPaymentTypeRepository paymentTypeRepository,
                                               IUserPaymentRepository userPaymentRepository)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _orderRepository = orderRepository;
            _paymentTypeRepository = paymentTypeRepository;
            _userPaymentRepository = userPaymentRepository;
        }

        public async Task<ApiResponse<object>> Handle(SavePaymentResultCommand request, CancellationToken cancellationToken)
        {
            var model = request.model;

            try
            {
                if (!model.IsSuccess || model.OrderId == Guid.Empty)
                    return ApiResponseBuilder.Error<object>("Dữ liệu thanh toán không hợp lệ");

                var order = await _orderRepository.GetByIdAsync(model.OrderId);
                if (order == null)
                    return ApiResponseBuilder.Error<object>("Không tìm thấy đơn hàng");

                // Tìm user payment nếu đã tồn tại
                var userPayment = await _userPaymentRepository.GetByUserIdAndProviderAsync
                    (model.UserId, model.Provider);

                // Nếu chưa có thì tạo mới
                if (userPayment == null)
                {
                    var paymentType = await _paymentTypeRepository.GetByValueAsync(model.Provider);
                    userPayment = new UserPayment
                    {
                        Id = Guid.NewGuid(),
                        UserId = order.UserId,
                        Provider = "VNPAY",
                        PaymentTypeId = paymentType?.Id ?? Guid.NewGuid()
                    };

                    await _userPaymentRepository.CreateAsync(userPayment);
                }

                var payment = new Payment
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    UserPaymentId = userPayment.Id,
                    TransactionId = model.TransactionId,
                    Amount = model.Amount,
                    Status = model.Status,
                    PaymentDate = DateTime.Now
                };

                await _unitOfWork.PaymentRepository.CreateAsync(payment);

                if (model.Status == PaymentStatus.Success)
                {
                    order.Status = OrderStatus.Completed;
                    _orderRepository.Update(order);
                }

                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>("", "Đã lưu kết quả thanh toán");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lưu kết quả thanh toán");
                return ApiResponseBuilder.Error<object>("Đã xảy ra lỗi khi lưu thanh toán");
            }
        }
    }
}
