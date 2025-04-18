using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.VariationManagement.Commands.Create
{
    public record CreateVariationCommand(CreateVariationDto model, string userName) : ICommand<ApiResponse<object>>;
}
