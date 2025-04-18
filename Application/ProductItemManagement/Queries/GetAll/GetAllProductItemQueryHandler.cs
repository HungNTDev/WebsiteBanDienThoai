using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.ProductItemManagement.Queries.GetAll
{
    public class GetAllProductItemQueryHandler :
        IQueryHandler<GetAllProductItemQuery, ApiResponse<PaginatedResult<GetAllProductItemDto>>>
    {
        private readonly IProductItemRepository _productItemRepository;
        private readonly ILogger<GetAllProductItemQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllProductItemQueryHandler(IMapper mapper,
                                             ILogger<GetAllProductItemQueryHandler> logger,
                                             IProductItemRepository productItemRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _productItemRepository = productItemRepository;
        }

        public async Task<ApiResponse<PaginatedResult<GetAllProductItemDto>>>
            Handle(GetAllProductItemQuery request, CancellationToken cancellationToken)
        {
            var model = request.model;
            try
            {
                var productItems = _productItemRepository.GetAll();
                var searchString = model.SearchTerm?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    _logger.LogInformation("Tìm kiếm với chuỗi: {SearchString}", searchString);
                    var normalizedSearch = searchString.Trim().ToLower();
                    productItems = productItems.Where(x =>
                        x.Name.ToLower().Contains(normalizedSearch)
                    );
                }
                if (!productItems.Any())
                {
                    return ApiResponseBuilder.Error<PaginatedResult<GetAllProductItemDto>>
                        ($"Không tìm thấy sản phẩm {searchString}", statusCode: 404);
                }
                var productItemsPaginated = PaginatedResult<ProductItem>.CreateAsync(
                    productItems,
                    model.PageIndex,
                    model.PageSize,
                    cancellationToken).Result;

                var productItemsForView = _mapper.Map<List<GetAllProductItemDto>>
                    (productItemsPaginated.Items);
                var paginatedProductItemsForView = new PaginatedResult<GetAllProductItemDto>(
                    productItemsForView,
                    productItemsPaginated.PageIndex,
                    productItemsPaginated.PageSize,
                    productItemsPaginated.TotalCount
                );
                return ApiResponseBuilder.Success(paginatedProductItemsForView,
                    "Lấy danh sách sản phẩm thành công");
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }
    }
}
