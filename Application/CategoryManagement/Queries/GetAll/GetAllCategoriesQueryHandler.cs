using Application.Abstract;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CategoryManagement.Queries.GetAll
{
    public class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery,
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
                if (!string.IsNullOrEmpty(searchString))
                {
                    _logger.LogInformation(searchString);
                    categories = categories.Where(x => EF.Functions.Unaccent(x.Name).ToLower().Trim()
                        .Contains(EF.Functions.Unaccent(searchString))
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

                var categoriesForView = _mapper.Map<List<GetAllCategoriesDto>>(categoriesPaginated.Items);
                
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
