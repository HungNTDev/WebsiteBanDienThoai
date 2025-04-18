using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using AutoMapper;

namespace Application.ProductManagement.Queries.GetWithOptions
{
    public class GetProductWithVariationsQueryHandler
        : IQueryHandler<GetProductWithVariationQuery, ApiResponse<ProductWithVariationsDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductWithVariationsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ProductWithVariationsDto>> Handle(GetProductWithVariationQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.id);

            if (product == null)
                return ApiResponseBuilder.Error<ProductWithVariationsDto>("Không tìm thấy sản phẩm");

            var variations = product.ProductItems
                .SelectMany(pi => pi.ProductConfigs)
                .Select(pc => pc.VariationOption)
                .GroupBy(vo => vo.Variation)
                .Select(g => new VariationDto
                {
                    Id = g.Key.Id,
                    Name = g.Key.Name,
                    Options = g.Select(o => new VariationOptionDto
                    {
                        Id = o.Id,
                        Name = o.Value
                    }).ToList()
                })
                .ToList();

            var dto = new ProductWithVariationsDto
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.Image,
                Price = product.Price,
                Variations = variations
            };

            return ApiResponseBuilder.Success<ProductWithVariationsDto>(dto, "");
        }
    }
}
