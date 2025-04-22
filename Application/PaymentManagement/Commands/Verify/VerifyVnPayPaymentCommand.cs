using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.PaymentManagement.ModelVnPay;

namespace Application.PaymentManagement.Commands.Verify
{
    public class VerifyVnPayPaymentCommand : ICommand<ApiResponse<VnPayCallbackResult>>
    {
        public Dictionary<string, string> QueryParams { get; set; } = new();

        public VerifyVnPayPaymentCommand(Dictionary<string, string> queryParams)
        {
            QueryParams = queryParams;
        }
    }
}
