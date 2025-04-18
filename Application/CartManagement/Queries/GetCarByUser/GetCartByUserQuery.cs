using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.CartManagement.Queries.GetCarByUser
{
    public record GetCartByUserQuery(Guid UserId) : IQuery<ApiResponse<GetCartByUserDto>>;
}
