using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.ProductManagement.Queries.GetAll
{
    public sealed class GetAllProductQueryHandler
        : IQueryHandler<GetAllProductQuery, ApiResponse<PaginatedResult<GetAllProductDto>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetAllProductQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllProductQueryHandler(
                                         IMapper mapper,
                                         IProductRepository productRepository,
                                         ILogger<GetAllProductQueryHandler> logger)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<PaginatedResult<GetAllProductDto>>> Handle(
            GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var filter = request.filter;
            try
            {
                var products = _productRepository.GetAll();
                var searchString = filter.SearchTerm?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    _logger.LogInformation("Tìm kiếm với chuỗi: {SearchString}", searchString);
                    var normalizedSearch = searchString.Trim().ToLower();
                    products = products.Where(x =>
                        x.Name.ToLower().Contains(normalizedSearch)
                    );
                }
                if (!products.Any())
                {
                    return ApiResponseBuilder.Error<PaginatedResult<GetAllProductDto>>
                        ($"Không tìm thấy sản phẩm {searchString}", statusCode: 404);
                }
                var productsPaginated = PaginatedResult<Product>.CreateAsync(
                    products,
                    filter.PageIndex,
                    filter.PageSize,
                    cancellationToken).Result;

                var productsForView = _mapper.Map<List<GetAllProductDto>>
                    (productsPaginated.Items);

                var paginatedProductsForView = new PaginatedResult<GetAllProductDto>(
                    productsForView,
                    productsPaginated.PageIndex,
                    productsPaginated.PageSize,
                    productsPaginated.TotalCount);

                return ApiResponseBuilder.Success(paginatedProductsForView, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Lỗi");
                throw;
            }
        }
    }
}
