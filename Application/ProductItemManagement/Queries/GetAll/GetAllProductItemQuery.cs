using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.ProductItemManagement.Queries.GetAll
{
    public record GetAllProductItemQuery(Filter model) :
        IQuery<ApiResponse<PaginatedResult<GetAllProductItemDto>>>;
}
