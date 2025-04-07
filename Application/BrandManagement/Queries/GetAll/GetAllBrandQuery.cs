using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.BrandManagement.Queries.GetAll
{
    public record GetAllBrandQuery(Filter filter)
        : IQuery<ApiResponse<PaginatedResult<GetAllBrandDto>>>;
}
