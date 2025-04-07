using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.InventoryManagement.Queries.GetAll
{
    public record GetAll_InventoryQuery(Filter filter)
        : IQuery<ApiResponse<PaginatedResult<GetAll_InventoryDto>>>;
}
