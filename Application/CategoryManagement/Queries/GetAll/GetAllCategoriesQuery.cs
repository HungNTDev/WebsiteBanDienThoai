using Application.Abstract.CQRS;
using Application.Abstract;

namespace Application.CategoryManagement.Queries.GetAll
{
    public record GetAllCategoriesQuery(Filter Filter) : IQuery<ApiResponse<PaginatedResult<GetAllCategoriesDto>>>;
}
