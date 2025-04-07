using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.ProductManagement.Commands.Update
{
    public record UpdateProductCommand(UpdateProductDto model, string userName)
        : ICommand<ApiResponse<object>>;
}
