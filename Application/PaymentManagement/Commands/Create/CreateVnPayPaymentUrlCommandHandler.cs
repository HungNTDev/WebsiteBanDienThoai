using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Services;

namespace Application.PaymentManagement.Commands.Create
{
    public class CreateVnPayPaymentUrlCommandHandler :
        ICommandHandler<CreateVnPayPaymentUrlCommand, ApiResponse<string>>
    {
        private readonly IVnPayService _vnPayService;

        public CreateVnPayPaymentUrlCommandHandler(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        public async Task<ApiResponse<string>> Handle(CreateVnPayPaymentUrlCommand request,
            CancellationToken cancellationToken)
        {
            var paymentUrl = await _vnPayService.GeneratePaymentUrl(request.model.OrderId, request.model.OrderTotal);
            return ApiResponseBuilder.Success(paymentUrl, "Create payment url successfully");
        }
    }
}
