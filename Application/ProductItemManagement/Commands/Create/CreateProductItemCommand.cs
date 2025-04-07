using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.ProductItemManagement.Commands.Create
{
    public record CreateProductItemCommand(CreateProductItemDto Model) : ICommand<ApiResponse<object>>;
}
