using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.ProductItemManagement.Commands.Update
{
    public record UpdateProductItemCommand(UpdateProductItemDto model, string userName)
        : ICommand<ApiResponse<object>>;
}
