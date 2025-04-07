using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.CategoryManagement.Commands.Update
{
    public record UpdateCategoryCommand(UpdateCategoryDto model, string userName) : ICommand<ApiResponse<object>>;
}
