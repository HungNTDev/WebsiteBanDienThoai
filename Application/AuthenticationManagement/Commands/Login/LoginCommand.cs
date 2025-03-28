using Application.Abstract;
using Application.Abstract.CQRS;

namespace Application.AuthenticationManagement.Commands.Login
{
    public record LoginCommand (LoginModel model) : ICommand<ApiResponse<object>>;
}
