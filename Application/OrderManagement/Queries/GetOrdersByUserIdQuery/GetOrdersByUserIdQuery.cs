using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.OrderManagement.Queries.GetOrdersByUserIdQuery
{
    public record GetOrdersByUserIdQuery(Guid UserId) : IQuery<List<GetOrdersByUserIdDto>>;
}
