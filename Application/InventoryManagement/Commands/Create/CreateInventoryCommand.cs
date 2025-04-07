using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.InventoryManagement.Commands.Create
{
    public record CreateInventoryCommand(CreateInventoryDto model, string userName)
        : ICommand<ApiResponse<object>>;
}
