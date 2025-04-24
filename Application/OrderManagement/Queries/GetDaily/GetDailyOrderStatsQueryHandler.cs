using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;

namespace Application.OrderManagement.Queries.GetDaily
{
    public class GetDailyOrderStatsQueryHandler : IQueryHandler<GetDailyOrderStatsQuery,
        ApiResponse<List<OrderDailyStatsDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetDailyOrderStatsQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<ApiResponse<List<OrderDailyStatsDto>>> Handle(GetDailyOrderStatsQuery request, CancellationToken cancellationToken)
        {
            var fromDate = DateTime.UtcNow.AddDays(-request.NumberOfDays);
            var orders = await _orderRepository.GetOrdersFromDateAsync(fromDate);
            var dailyStats = orders
                .GroupBy(o => o.CreatedDate.Date)
                .Select(g => new OrderDailyStatsDto
                {
                    Date = g.Key,
                    OrderCount = g.Count()
                })
                .ToList();
            return ApiResponseBuilder.Success(dailyStats, "");
        }
    }
}
