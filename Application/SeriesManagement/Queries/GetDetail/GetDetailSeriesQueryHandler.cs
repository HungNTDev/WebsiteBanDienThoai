using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using AutoMapper;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Application.SeriesManagement.Queries.GetDetail
{
    public record GetDetailSeriesQueryHandler :
        IQueryHandler<GetDetailSeriesQuery, OneOf<ApiResponse<GetDetailSeriesDto>, GetDetailSeriesDto>>
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetDetailSeriesQueryHandler> _logger;
        private readonly IMapper _mapper;
        public GetDetailSeriesQueryHandler(
            ILogger<GetDetailSeriesQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ISeriesRepository seriesRepository)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _seriesRepository = seriesRepository;
        }

        public async Task<OneOf<ApiResponse<GetDetailSeriesDto>, GetDetailSeriesDto>>
            Handle(GetDetailSeriesQuery request, CancellationToken cancellationToken)
        {
            var seriesId = request.Id;
            try
            {
                var series = await _seriesRepository.GetByIdAsync(seriesId);
                if (series == null)
                {
                    return
                        ApiResponseBuilder.Error<GetDetailSeriesDto>("Không tìm thấy bộ sưu tập",
                        statusCode: 404);
                }
                var seriesForView = _mapper.Map<GetDetailSeriesDto>(series);
                Console.WriteLine(seriesForView);
                return seriesForView;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
