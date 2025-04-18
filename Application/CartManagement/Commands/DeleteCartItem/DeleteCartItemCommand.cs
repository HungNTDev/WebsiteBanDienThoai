using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.CartManagement.Commands.DeleteCartItem
{
    public record DeleteCartItemCommand(DeleteCartItemDto model) : ICommand<ApiResponse<object>>;
}
