using Application.Abstract;
using Application.Abstract.CQRS;

namespace Application.BrandManagement.Commands.Update
{
    public record UpdateBrandCommand(UpdateBrandDto model, string userName)
        : ICommand<ApiResponse<object>>;
}
