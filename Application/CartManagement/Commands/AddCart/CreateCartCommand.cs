using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.CartManagement.Commands.AddCart
{
    public record CreateCartCommand(CreateCartDto model) : ICommand<ApiResponse<object>>
    {
    }
}
