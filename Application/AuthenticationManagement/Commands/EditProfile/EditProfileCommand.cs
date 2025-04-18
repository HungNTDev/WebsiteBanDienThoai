using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using System.Windows.Input;

namespace Application.AuthenticationManagement.Commands.EditProfile
{
    public record EditProfileCommand(EditProfileDto model, string id) : ICommand<ApiResponse<object>>;
}
