using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.CategoryManagement.Commands.Create
{
    public record CreateCategoryCommand(CreateCategoryDto model, string userEmail) : ICommand<ApiResponse<object>>;
}
