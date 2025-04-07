using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.SeriesManagement.Queries.GetAll
{
    public record GetAllSeriesQuery(Filter filter)
        : IQuery<ApiResponse<PaginatedResult<GetAllSeriesDto>>>;
}
