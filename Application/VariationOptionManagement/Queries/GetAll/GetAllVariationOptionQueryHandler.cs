using Application.Abstract;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.VariationOptionManagement.Queries.GetAll
{
    public class GetAllVariationOptionQueryHandler
        : IQueryHandler<GetAllVariationOptionQuery,
            ApiResponse<PaginatedResult<GetAllVariationOptionDto>>>
    {
        private readonly IVariationOptionRepository _variationOptionRepository;
        private readonly ILogger<GetAllVariationOptionQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllVariationOptionQueryHandler(IMapper mapper,
                                                 ILogger<GetAllVariationOptionQueryHandler> logger,
                                                 IVariationOptionRepository variationOptionRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _variationOptionRepository = variationOptionRepository;
        }

        public async Task<ApiResponse<PaginatedResult<GetAllVariationOptionDto>>>
            Handle(GetAllVariationOptionQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            try
            {
                var variationOptions = _variationOptionRepository.GetAll();
                var variationOptionSearch = filter.SearchTerm?.Trim().ToLower();
                if (!string.IsNullOrEmpty(variationOptionSearch))
                {
                    _logger.LogInformation(variationOptionSearch);
                    var normalizedSearch = variationOptionSearch.Trim().ToLower();
                    variationOptions = variationOptions.Where(x =>
                        x.Value.ToLower().Contains(normalizedSearch)
                    );
                }
                if (!variationOptions.Any())
                {
                    return ApiResponseBuilder
                        .Error<PaginatedResult<GetAllVariationOptionDto>>
                        ($"Không tìm thấy biến thể {variationOptionSearch}",
                        statusCode: 404);
                }
                var variationOptionPaginated = await PaginatedResult<VariationOption>
                    .CreateAsync(variationOptions, filter.PageIndex, filter.PageSize, cancellationToken);
                var variationOptionsForView = _mapper.Map<List<GetAllVariationOptionDto>>
                    (variationOptionPaginated.Items);

                var paginatedVariationOptionsForView = new PaginatedResult<GetAllVariationOptionDto>(
                    variationOptionsForView,
                    variationOptionPaginated.TotalCount,
                    variationOptionPaginated.PageIndex,
                    variationOptionPaginated.PageSize);
                return ApiResponseBuilder.Success(paginatedVariationOptionsForView, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Lỗi");
                throw;
            }
        }
    }
}
