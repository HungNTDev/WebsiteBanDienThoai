using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Application.SeriesManagement.Queries.GetAll
{
    public record GetAllSeriesQueryHandler
        : IQueryHandler<GetAllSeriesQuery, ApiResponse<PaginatedResult<GetAllSeriesDto>>>
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllSeriesQueryHandler> _logger;
        public GetAllSeriesQueryHandler(ILogger<GetAllSeriesQueryHandler> logger,
            ISeriesRepository seriesRepository,
            IMapper mapper)
        {
            _logger = logger;
            _seriesRepository = seriesRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<PaginatedResult<GetAllSeriesDto>>>
            Handle(GetAllSeriesQuery request, CancellationToken cancellationToken)
        {
            var filter = request.filter;
            try
            {
                var series = _seriesRepository.GetAll();
                var searchingSeries = filter.SearchTerm?.Trim().ToLower();
                if (!string.IsNullOrEmpty(searchingSeries))
                {
                    _logger.LogInformation(searchingSeries);
                    var normalizedSearch = searchingSeries.Trim().ToLower();
                    series = series.Where(x =>
                        x.Name.ToLower().Contains(normalizedSearch)
                    );
                }
                if (!series.Any())
                {
                    return ApiResponseBuilder
                         .Error<PaginatedResult<GetAllSeriesDto>>
                         ($"Không tìm thấy bộ sưu tập {searchingSeries}",
                         statusCode: 404);
                }

                var seriesPaginated = PaginatedResult<Domain.Entities.Series>
                    .CreateAsync(series, filter.PageIndex, filter.PageSize, cancellationToken);

                var seriesForView = _mapper.Map<List<GetAllSeriesDto>>
                    (seriesPaginated.Result.Items);

                var paginatedSeriesForView = new PaginatedResult<GetAllSeriesDto>(
                    seriesForView,
                    seriesPaginated.Result.TotalCount,
                    seriesPaginated.Result.PageIndex,
                    seriesPaginated.Result.PageSize);

                return ApiResponseBuilder.Success(paginatedSeriesForView, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Lỗi");
                throw;
            }
        }
    }
}
