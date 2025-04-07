using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.ProductItemManagement.Queries.GetByOptions
{
    public record GetProductItemQuery(Guid ProductId, List<Guid> OptionIds)
        : IQuery<ApiResponse<GetProductItemDto>>;
}
