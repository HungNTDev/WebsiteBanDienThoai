using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.CartManagement.Commands.DeleteCart
{
    public record DeleteCartCommand(DeleteCartDto model) : ICommand<ApiResponse<object>>;
}
