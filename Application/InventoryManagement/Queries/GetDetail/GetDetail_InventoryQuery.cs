using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using OneOf;

namespace Application.InventoryManagement.Queries.GetDetail
{
    public record GetDetail_InventoryQuery(Guid id)
        : IQuery<OneOf<ApiResponse<GetDetail_InventoryDto>, GetDetail_InventoryDto>>;

}
