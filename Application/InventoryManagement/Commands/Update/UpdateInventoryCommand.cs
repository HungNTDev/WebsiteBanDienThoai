using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.InventoryManagement.Commands.Update
{
    public record UpdateInventoryCommand(UpdateInventoryDto model, string userName)
        : ICommand<ApiResponse<object>>;
}
