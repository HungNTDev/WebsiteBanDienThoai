using Application.Abstract;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Application.ProductManagement.Queries.GetDetail
{
    public sealed class GetDetailProductQueryHandler :
        IQueryHandler<GetDetailProductQuery, ApiResponse<GetDetailProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDetailProductQueryHandler> _logger;
        private readonly IProductRepository _productRepository;
        public GetDetailProductQueryHandler(ILogger<GetDetailProductQueryHandler> logger,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _logger = logger;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<GetDetailProductDto>> Handle(GetDetailProductQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id;
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product is null)
                {
                    return ApiResponseBuilder.Error<GetDetailProductDto>("Không tìm thấy sản phẩm", statusCode: 404);
                }
                var productDto = _mapper.Map<GetDetailProductDto>(product);
                return ApiResponseBuilder.Success(productDto, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while Get Product Details with Id: {id}");
                return ApiResponseBuilder.Error<GetDetailProductDto>("An unexpected error occurred", statusCode: 500);
            }
        }
    }
}
