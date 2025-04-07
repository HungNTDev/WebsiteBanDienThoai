using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.VariationOptionManagement.Commands.Create
{
    public record CreateVariationOptionCommand(CreateVariationOptionDto model, string userName)
    : ICommand<ApiResponse<object>>;
}
