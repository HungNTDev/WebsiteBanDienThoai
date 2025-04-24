using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.OrderManagement.Queries.GetDaily
{
    public class GetDailyOrderStatsQuery : IQuery<ApiResponse<List<OrderDailyStatsDto>>>
    {
        public int NumberOfDays { get; set; }

        public GetDailyOrderStatsQuery() { }

        public GetDailyOrderStatsQuery(int numberOfDays)
        {
            NumberOfDays = numberOfDays;
        }
    }

}
