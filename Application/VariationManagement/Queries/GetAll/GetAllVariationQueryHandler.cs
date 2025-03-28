using Application.Abstract;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.VariationManagement.Queries.GetAll
{
    public class GetAllVariationQueryHandler :
        IQueryHandler<GetAllVariationQuery, ApiResponse<PaginatedResult<GetAllVariationDto>>>
    {
        private readonly IVariationRepository _variationRepository;
        private readonly ILogger<GetAllVariationQueryHandler> _logger;
        private IMapper _mapper;

        public GetAllVariationQueryHandler(ILogger<GetAllVariationQueryHandler> logger,
            IVariationRepository variationRepository,
            IMapper mapper)
        {
            _logger = logger;
            _variationRepository = variationRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PaginatedResult<GetAllVariationDto>>>
            Handle(GetAllVariationQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            try
            {
                var variations = _variationRepository.GetAll();
                var searchingVariations = filter.SearchTerm?.Trim().ToLower();

                if (!string.IsNullOrEmpty(searchingVariations))
                {
                    _logger.LogInformation(searchingVariations);
                    variations = variations.Where(x => EF.Functions.Unaccent(x.Name.ToLower())
                        .Contains(EF.Functions.Unaccent(searchingVariations)));
                }

                if (!variations.Any())
                {
                    return ApiResponseBuilder
                         .Error<PaginatedResult<GetAllVariationDto>>
                         ($"Không tìm thấy biến thể {searchingVariations}",
                         statusCode: 404);
                }

                var variationPaginated = await PaginatedResult<Variation>
                    .CreateAsync(variations, filter.PageIndex, filter.PageSize, cancellationToken);

                var variationsForView = _mapper.Map<List<GetAllVariationDto>>
                    (variationPaginated.Items);

                var paginatedVariationsForView = new PaginatedResult<GetAllVariationDto>(
                    variationsForView,
                    filter.PageIndex,
                    filter.PageSize,
                    variationPaginated.TotalCount);

                return ApiResponseBuilder.Success(paginatedVariationsForView, "");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Lỗi");
                throw;
            }
        }
    }
}
