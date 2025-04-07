using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.AuthenticationManagement.Commands.Login
{
    public record LoginCommand (LoginModel model) : ICommand<ApiResponse<object>>;
}
