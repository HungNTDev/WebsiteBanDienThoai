using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.CategoryManagement.Queries.GetAll
{
    public sealed class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery,
        ApiResponse<PaginatedResult<GetAllCategoriesDto>>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetAllCategoriesQueryHandler> _logger;
        private IMapper _mapper;

        public GetAllCategoriesQueryHandler(ILogger<GetAllCategoriesQueryHandler> logger,
                                            ICategoryRepository categoryRepository,
                                            IMapper mapper)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PaginatedResult<GetAllCategoriesDto>>>
            Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            try
            {
                var categories = _categoryRepository.GetAll();
                var searchString = filter.SearchTerm?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    _logger.LogInformation("Tìm kiếm với chuỗi: {SearchString}", searchString);

                    var normalizedSearch = searchString.Trim().ToLower();

                    categories = categories.Where(x =>
                        x.Name.ToLower().Contains(normalizedSearch)
                    );
                }

                if (!categories.Any())
                {
                    return ApiResponseBuilder.Error<PaginatedResult<GetAllCategoriesDto>>
                        ($"Không tìm thấy thể loại {searchString}", statusCode: 404);
                }

                var categoriesPaginated = await PaginatedResult<Category>.CreateAsync(
                    categories,
                    filter.PageIndex,
                    filter.PageSize,
                    cancellationToken);

                var categoriesForView = _mapper.Map<List<GetAllCategoriesDto>>
                    (categoriesPaginated.Items);

                var paginatedCategoriesForView = new PaginatedResult<GetAllCategoriesDto>(
                    categoriesForView,
                    filter.PageIndex,
                    filter.PageSize,
                    categoriesPaginated.TotalCount);

                return ApiResponseBuilder.Success(paginatedCategoriesForView, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Lỗi");
                throw;
            }
        }
    }
}
