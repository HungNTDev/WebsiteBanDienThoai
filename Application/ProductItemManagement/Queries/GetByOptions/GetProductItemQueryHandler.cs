using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Application.ProductItemManagement.Queries.GetByOptions
{
    public sealed class GetProductItemQueryHandler
   : IQueryHandler<GetProductItemQuery, ApiResponse<GetProductItemDto>>
    {
        private readonly ILogger<GetProductItemQueryHandler> _logger;
        private readonly IProductItemRepository _productItemRepository;
        private readonly IMapper _mapper;
        public GetProductItemQueryHandler(
            ILogger<GetProductItemQueryHandler> logger,
            IProductItemRepository productItemRepository,
            IMapper mapper)
        {
            _logger = logger;
            _productItemRepository = productItemRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<GetProductItemDto>> Handle(GetProductItemQuery request, CancellationToken cancellationToken)
        {
            var model = request;
            try
            {
                var product = await _productItemRepository.GetProductItemByOptionsAsync(model.ProductId, model.OptionIds);
                if (product is null)
                {
                    return ApiResponseBuilder.Error<GetProductItemDto>("Không tìm thấy sản phẩm", statusCode: 404);
                }
                var productItemDto = _mapper.Map<GetProductItemDto>(product);
                return ApiResponseBuilder.Success(productItemDto, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while get product item by options");
                return ApiResponseBuilder.Error<GetProductItemDto>("An unexpected error occurred", statusCode: 500);
            }
        }
    }
}
