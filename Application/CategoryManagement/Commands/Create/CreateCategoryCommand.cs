using Application.Abstract;
using Application.Abstract.CQRS;

namespace Application.CategoryManagement.Commands.Create
{
    public record CreateCategoryCommand(CreateCategoryDto model, string userEmail) : ICommand<ApiResponse<object>>;
}
