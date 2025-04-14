using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.AuthenticationManagement.Commands.Google
{
    public record GoogleLoginRequestCommand(GoogleLoginRequestDto model) : ICommand<ApiResponse<object>>;
}
