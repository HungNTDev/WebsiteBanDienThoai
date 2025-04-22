using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Services;
using Application.PaymentManagement.ModelVnPay;

namespace Application.PaymentManagement.Commands.Verify
{
    public class VerifyVnPayPaymentCommandHandler : ICommandHandler<VerifyVnPayPaymentCommand, ApiResponse<VnPayCallbackResult>>
    {
        private readonly IVnPayService _vnPayService;

        public VerifyVnPayPaymentCommandHandler(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        public async Task<ApiResponse<VnPayCallbackResult>> Handle(VerifyVnPayPaymentCommand request, CancellationToken cancellationToken)
        {
            var result = _vnPayService.VerifyPayment(request.QueryParams);
            return ApiResponseBuilder.Success(result, "Verify successfully");
        }
    }

}
