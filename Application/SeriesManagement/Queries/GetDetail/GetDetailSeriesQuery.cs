using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using OneOf;

namespace Application.SeriesManagement.Queries.GetDetail
{
    public record GetDetailSeriesQuery(Guid Id)
        : IQuery<OneOf<ApiResponse<GetDetailSeriesDto>, GetDetailSeriesDto>>;
}
