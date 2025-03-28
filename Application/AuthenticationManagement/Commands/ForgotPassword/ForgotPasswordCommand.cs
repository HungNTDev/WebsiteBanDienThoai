using Application.Abstract.CQRS;
using Application.Abstract;

namespace Application.AuthenticationManagement.Commands.ForgotPassword
{
    public record  ForgotPasswordCommand(ForgotPasswordModel Model) : ICommand<ApiResponse<object>>;
}
