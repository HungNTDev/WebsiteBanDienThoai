using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.CartManagement.Commands.UpdateCart
{
    public record UpdateCartCommand(UpdateCartDto model) : ICommand<ApiResponse<object>>;
}
