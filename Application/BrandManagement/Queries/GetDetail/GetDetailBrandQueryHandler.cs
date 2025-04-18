using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using AutoMapper;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Application.BrandManagement.Queries.GetDetail
{
    public record GetDetailBrandQueryHandler :
        IQueryHandler<GetDetailBrandQuery, OneOf<ApiResponse<GetDetailBrandDto>, GetDetailBrandDto>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ILogger<GetDetailBrandQueryHandler> _logger;
        private IMapper _mapper;
        public GetDetailBrandQueryHandler(ILogger<GetDetailBrandQueryHandler> logger,
            IBrandRepository brandRepository,
            IMapper mapper)
        {
            _logger = logger;
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public async Task<OneOf<ApiResponse<GetDetailBrandDto>, GetDetailBrandDto>> Handle(GetDetailBrandQuery request, CancellationToken cancellationToken)
        {
            var brandId = request.Id;
            try
            {
                var brand = await _brandRepository.GetByIdAsync(brandId);
                if (brand == null)
                {
                    return ApiResponseBuilder
                        .Error<GetDetailBrandDto>
                        ($"Không tìm thấy thương hiệu với id {brandId}",
                        statusCode: 404);
                }
                var brandForView = _mapper.Map<GetDetailBrandDto>(brand);
                return brandForView;
            }
            catch (Exception ex)
            {
                return ApiResponseBuilder.Error<GetDetailBrandDto>(ex.Message);
                throw;
            }
        }
    }
}
