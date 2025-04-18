using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.ProductItemManagement.Commands.Create;
using Domain.Abstract;
using Microsoft.Extensions.Logging;

namespace Application.ProductItemManagement.Queries.GetById
{
    public class GetProductItemQueryHandler : IQueryHandler<GetProductItemByIdQuery, ApiResponse<ProductItemDto>>
    {
        private readonly IProductItemRepository _productItemRepository;
        private readonly ILogger<GetProductItemQueryHandler> _logger;

        public GetProductItemQueryHandler(IProductItemRepository productItemRepository, ILogger<GetProductItemQueryHandler> logger)
        {
            _productItemRepository = productItemRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<ProductItemDto>> Handle(GetProductItemByIdQuery request, CancellationToken cancellationToken)
        {
            var model = request.Id;
            try
            {

                var productItem = await _productItemRepository.GetByIdAsync(model);
                if (productItem == null)
                {
                    return ApiResponseBuilder.Error<ProductItemDto>("Product item not found", statusCode: 404);
                }

                var productItemDto = new ProductItemDto
                {
                    Id = productItem.Id,
                    SKU = productItem.SKU,
                    Price = productItem.Price,
                    ProductName = productItem.ProductName,
                    Image = productItem.Image,
                    VariationOptions = productItem.VariationOptions.ToList()
                };
                return ApiResponseBuilder.Success(productItemDto, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting product item by ID");
                return ApiResponseBuilder.Error<ProductItemDto>("An unexpected error occurred", statusCode: 500);
            }
        }
    }
}
