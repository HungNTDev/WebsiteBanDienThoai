using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using System.Windows.Input;

namespace Application.BrandManagement.Commands.Create
{
    public record CreateBrandCommand(CreateBrandDto model, string userName)
        : ICommand<ApiResponse<object>>;
}
