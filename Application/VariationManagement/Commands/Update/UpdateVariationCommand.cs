using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.VariationManagement.Commands.Update
{
    public record class UpdateVariationCommand(UpdateVariationDto model, string userName)
        : ICommand<ApiResponse<object>>;
}
