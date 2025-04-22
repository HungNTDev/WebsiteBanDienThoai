using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.PaymentManagement.Commands.Save
{
    public record SavePaymentResultCommand(SavePaymentResultDto model) : ICommand<ApiResponse<object>>
    {
    }
}
