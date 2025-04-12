using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Application.VariationOptionManagement.Queries.GetById
{
    public record GetVariationOptionByIdQueryHandler : IQueryHandler<GetVariationOptionByIdQuery,
        OneOf<ApiResponse<GetVariationOptionByIdDto>, GetVariationOptionByIdDto>>
    {
        private readonly IVariationOptionRepository _variationOptionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetVariationOptionByIdQueryHandler> _logger;
        public GetVariationOptionByIdQueryHandler(ILogger<GetVariationOptionByIdQueryHandler> logger,
            IVariationOptionRepository variationOptionRepository,
            IMapper mapper)
        {
            _logger = logger;
            _variationOptionRepository = variationOptionRepository;
            _mapper = mapper;
        }
        public async Task<OneOf<ApiResponse<GetVariationOptionByIdDto>, GetVariationOptionByIdDto>> Handle(GetVariationOptionByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                VariationOption variationOption = await _variationOptionRepository.GetByIdAsync(request.Id);
                if (variationOption == null)
                {
                    return ApiResponseBuilder.Error<GetVariationOptionByIdDto>("Không tìm thấy biến thể", statusCode: 404);
                }
                GetVariationOptionByIdDto variationOptionForView = _mapper.Map<GetVariationOptionByIdDto>(variationOption);
                return variationOptionForView;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
