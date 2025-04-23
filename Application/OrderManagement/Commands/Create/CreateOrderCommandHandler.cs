using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using Application.Abstract.Services;
using Domain.Entities;
using Domain.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.OrderManagement.Commands.Create
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, ApiResponse<object>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly IUserPaymentRepository _userPaymentRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IVnPayService _vnPayService;
        private readonly IConfiguration _config;
        private readonly IPayPalService _paypalService;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository,
                                         IUnitOfWork unitOfWork,
                                         ILogger<CreateOrderCommandHandler> logger,
                                         IVnPayService vnPayService,
                                         IPaymentRepository paymentRepository,
                                         IUserPaymentRepository userPaymentRepository,
                                         IPaymentTypeRepository paymentTypeRepository,
                                         IConfiguration config,
                                         IPayPalService paypalService,
                                         IUserRepository userRepository,
                                         IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _vnPayService = vnPayService;
            _paymentRepository = paymentRepository;
            _userPaymentRepository = userPaymentRepository;
            _paymentTypeRepository = paymentTypeRepository;
            _config = config;
            _paypalService = paypalService;
            _userRepository = userRepository;
            _emailService = emailService;
        }
        public async Task<ApiResponse<object>> Handle(CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.model;

            try
            {
                // 1. Tạo đơn hàng
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    UserId = dto.UserId,
                    Code = OrderCodeGenerator.GenerateOrderCode(),
                    OrderDate = DateTime.UtcNow,
                    OrderTotal = dto.OrderTotal,
                    Status = OrderStatus.Pending,
                    OrderItems = dto.Items.Select(i => new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductItemId = i.ProductItemId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                    }).ToList()
                };

                await _orderRepository.CreateAsync(order);

                // 2. Lấy phương thức thanh toán
                var paymentType = await _paymentTypeRepository.GetByIdAsync(dto.PaymentTypeId);
                if (paymentType == null)
                    return ApiResponseBuilder.Error<object>("Phương thức thanh toán không hợp lệ");

                // 3. Tạo hoặc lấy UserPayment
                var userPayment = await _userPaymentRepository.GetByUserAndTypeAsync(dto.UserId, dto.PaymentTypeId);

                if (userPayment == null)
                {
                    userPayment = new UserPayment
                    {
                        Id = Guid.NewGuid(),
                        UserId = dto.UserId,
                        PaymentTypeId = dto.PaymentTypeId,
                        Provider = paymentType.Value
                    };

                    await _userPaymentRepository.CreateAsync(userPayment);
                    await _unitOfWork.SaveChangesAsync();
                }

                // 4. Tạo Payment
                var payment = new Payment
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    UserPaymentId = userPayment.Id,
                    Amount = dto.OrderTotal
                };

                string? redirectUrl = null;

                switch (paymentType.Code)
                {
                    case PaymentTypeCode.CASH:
                        payment.Status = PaymentStatus.Pending;
                        payment.TransactionId = "COD";
                        order.Status = OrderStatus.Pending;
                        break;

                    case PaymentTypeCode.VNPAY:
                        payment.Status = PaymentStatus.Success;
                        payment.TransactionId = "VNPAY_INIT";
                        order.Status = OrderStatus.Confirmed;
                        redirectUrl = await _vnPayService.
                            GeneratePaymentUrl(order.Id, (decimal)payment.Amount);
                        break;

                    case PaymentTypeCode.PAYPAL:
                        payment.Status = PaymentStatus.Success;
                        payment.TransactionId = "PAYPAL_INIT";
                        order.Status = OrderStatus.Confirmed;
                        var returnUrl = _config["PayPal:ReturnUrl"] + $"?orderId={order.Id}";
                        var cancelUrl = _config["PayPal:CancelUrl"] ?? returnUrl;

                        redirectUrl = await _paypalService.
                            CreateOrderAsync((decimal)payment.Amount, returnUrl,
                            cancelUrl);
                        if (string.IsNullOrEmpty(redirectUrl))
                        {
                            return ApiResponseBuilder.Error<object>("Không tạo được đơn hàng PayPal");
                        }
                        break;

                    default:
                        return ApiResponseBuilder.Error<object>("Phương thức thanh toán không hợp lệ");
                }

                await _paymentRepository.CreateAsync(payment);
                await _unitOfWork.SaveChangesAsync();

                var user = await _userRepository.GetByIdAsync(dto.UserId);
                if (user != null && !string.IsNullOrWhiteSpace(user.Email))
                {
                    var subject = "Xác nhận đơn hàng thành công";
                    var body = $@"<p>Xin chào {user.FullName ?? "khách hàng"},</p>
                             <p>Đơn hàng <strong>{order.Code}</strong> đã được tạo thành công.</p>
                             <p>Tổng tiền: <strong>{order.OrderTotal:N0} đ</strong></p>
                             <p>Phương thức thanh toán: <strong>{paymentType.Value}</strong></p>
                             <p>Cảm ơn bạn đã mua sắm tại cửa hàng của chúng tôi.</p>";
                    await _emailService.SendAsync(user.Email, subject, body);
                }

                return redirectUrl != null
                    ? ApiResponseBuilder.Success<object>(new { Url = redirectUrl }, "Tạo đơn hàng thành công")
                    : ApiResponseBuilder.Success<object>(order, "Tạo đơn hàng thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Create Order");
                return ApiResponseBuilder.Error<object>("Có lỗi xảy ra", statusCode: 500);
            }
        }
    }
    public static class OrderCodeGenerator
    {
        public static string GenerateOrderCode()
        {
            var prefix = "ORD"; // Hoặc bạn có thể đặt là "DH" nếu muốn viết tiếng Việt
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss"); // 20250421103015
            var random = new Random().Next(1000, 99999); // 4 chữ số ngẫu nhiên
            return $"{prefix}{timestamp}{random}";
        }
    }
}
