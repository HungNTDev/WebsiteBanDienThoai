using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using System.Windows.Input;

namespace Application.PaymentManagement.Commands.Create
{
    public record CreateVnPayPaymentUrlCommand(CreateVnPayPaymentUrlDto model)
        : ICommand<ApiResponse<string>>;
}
