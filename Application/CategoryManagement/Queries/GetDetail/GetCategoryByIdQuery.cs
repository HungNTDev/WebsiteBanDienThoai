using Application.Abstract;
using Application.Abstract.CQRS;
using OneOf;

namespace Application.CategoryManagement.Queries.GetDetail
{
    public record GetCategoryByIdQuery(Guid id)
        : IQuery<OneOf<ApiResponse<GetCategoryDto>, GetCategoryDto>>;
}
