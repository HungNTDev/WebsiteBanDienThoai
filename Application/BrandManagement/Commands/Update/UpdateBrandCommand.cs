using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.BrandManagement.Commands.Update
{
    public record UpdateBrandCommand(UpdateBrandDto model, string userName)
        : ICommand<ApiResponse<object>>;
}
