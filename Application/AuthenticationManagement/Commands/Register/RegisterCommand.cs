using Application.Abstract.CQRS;
using Application.Abstract;

namespace Application.AuthenticationManagement.Commands.Register
{
    public record RegisterCommand(RegisterModel model) : ICommand<ApiResponse<object>>;
}
