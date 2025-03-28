using Application.Abstract;
using Application.Abstract.CQRS;

namespace Application.CategoryManagement.Commands.Update
{
    public record UpdateCategoryCommand(UpdateCategoryDto model) : ICommand<ApiResponse<object>>;
}
