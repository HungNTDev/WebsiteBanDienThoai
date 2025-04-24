using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.OrderManagement.Queries.GetOrderHistory
{
    public record GetOrderHistoryQuery : IQuery<ApiResponse<List<OrderHistoryDto>>> { }
}
