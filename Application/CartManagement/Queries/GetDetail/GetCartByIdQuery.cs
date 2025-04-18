using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using OneOf;

namespace Application.CartManagement.Queries.GetCartById
{
    public record GetCartByIdQuery(Guid id) : IQuery<OneOf<GetCartByIdDto, ApiResponse<GetCartByIdDto>>>
    {
    }
}
