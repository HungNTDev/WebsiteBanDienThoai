using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.OrderManagement.Queries.GetOrderById
{
    public record GetOrderByIdQuery(Guid Id) : IQuery<ApiResponse<GetOrderByIdDto>>
    {
    }
}
