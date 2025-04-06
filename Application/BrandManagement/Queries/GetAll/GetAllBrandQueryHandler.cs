using Application.Abstract;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.VariationManagement.Queries.GetAll;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.BrandManagement.Queries.GetAll
{
    public class GetAllBrandQueryHandler
        : IQueryHandler<GetAllBrandQuery, ApiResponse<PaginatedResult<GetAllBrandDto>>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ILogger<GetAllBrandQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllBrandQueryHandler(IMapper mapper,
                                       ILogger<GetAllBrandQueryHandler> logger,
                                       IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _brandRepository = brandRepository;
        }

        public async Task<ApiResponse<PaginatedResult<GetAllBrandDto>>>
            Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
            var filter = request.filter;
            try
            {
                var brands = _brandRepository.GetAll();
                var searchingVariations = filter.SearchTerm?.Trim().ToLower();

                if (!string.IsNullOrEmpty(searchingVariations))
                {
                    _logger.LogInformation(searchingVariations);
                    var normalizedSearch = searchingVariations.Trim().ToLower();

                    brands = brands.Where(x =>
                        x.Name.ToLower().Contains(normalizedSearch)
                    );
                }

                if (!brands.Any())
                {
                    return ApiResponseBuilder
                         .Error<PaginatedResult<GetAllBrandDto>>
                         ($"Không tìm thấy biến thể {searchingVariations}",
                         statusCode: 404);
                }

                var brandPaginated = await PaginatedResult<Brand>
                    .CreateAsync(brands, filter.PageIndex, filter.PageSize, cancellationToken);

                var brandsForView = _mapper.Map<List<GetAllBrandDto>>
                    (brandPaginated.Items);

                var paginatedBrandsForView = new PaginatedResult<GetAllBrandDto>(
                    brandsForView,
                    brandPaginated.TotalCount,
                    brandPaginated.PageIndex,
                    brandPaginated.PageSize);

                return ApiResponseBuilder.Success(paginatedBrandsForView,
                    "Lấy danh sách thương hiệu thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Lỗi");
                throw;
            }
        }
    }
}
