using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.ProductItemManagement.Commands.Create;

namespace Application.ProductItemManagement.Queries.GetById
{
    public record GetProductItemByIdQuery
        (Guid Id)
        : IQuery<ApiResponse<ProductItemDto>>;
}
