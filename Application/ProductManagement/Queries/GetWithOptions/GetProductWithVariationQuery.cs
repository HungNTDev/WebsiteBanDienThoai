using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.ProductManagement.Queries.GetWithOptions
{
    public record GetProductWithVariationQuery(Guid id) : IQuery<ApiResponse<ProductWithVariationsDto>>;
}
