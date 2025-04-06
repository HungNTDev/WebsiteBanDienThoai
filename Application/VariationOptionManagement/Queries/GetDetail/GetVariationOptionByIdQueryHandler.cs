using Application.Abstract;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.VariationManagement.Queries.GetById;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Application.VariationOptionManagement.Queries.GetById
{
    public record GetVariationOptionByIdQueryHandler : IQueryHandler<GetVariationOptionByIdQuery,
        OneOf<ApiResponse<GetVariationOptionDto>, GetVariationOptionDto>>
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
        public async Task<OneOf<ApiResponse<GetVariationOptionDto>, GetVariationOptionDto>> Handle(GetVariationOptionByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                VariationOption variationOption = await _variationOptionRepository.GetByIdAsync(request.Id);
                if (variationOption == null)
                {
                    return ApiResponseBuilder.Error<GetVariationOptionDto>("Không tìm thấy biến thể", statusCode: 404);
                }
                GetVariationOptionDto variationOptionForView = _mapper.Map<GetVariationOptionDto>(variationOption);
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
