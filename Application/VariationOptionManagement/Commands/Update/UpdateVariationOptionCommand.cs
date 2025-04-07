using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.VariationOptionManagement.Commands.Update
{
    public record UpdateVariationOptionCommand(UpdateVariationOptionDto model, string userName)
        : ICommand<ApiResponse<object>>;
}
