using Application.Abstract;
using Application.Abstract.CQRS;

namespace Application.VariationManagement.Commands.Create
{
    public record CreateVariationCommand(CreateVariationDto model) : ICommand<ApiResponse<object>>;
}
