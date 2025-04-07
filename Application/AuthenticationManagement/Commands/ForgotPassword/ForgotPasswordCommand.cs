using Application.Abstract.CQRS;
using Application.Abstract.BaseClass;

namespace Application.AuthenticationManagement.Commands.ForgotPassword
{
    public record  ForgotPasswordCommand(ForgotPasswordModel Model) : ICommand<ApiResponse<object>>;
}
