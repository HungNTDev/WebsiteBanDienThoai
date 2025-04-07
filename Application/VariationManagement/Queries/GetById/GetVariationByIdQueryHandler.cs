using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Application.VariationManagement.Queries.GetById
{
    public record GetVariationByIdQueryHandler :
        IQueryHandler<GetVariationByIdQuery,
            OneOf<ApiResponse<GetVariationOptionDto>, GetVariationOptionDto>>
    {
        private readonly IMapper _mapper;
        private readonly IVariationRepository _variationRepository;
        private readonly ILogger<GetVariationByIdQueryHandler> _logger;

        public GetVariationByIdQueryHandler(
            ILogger<GetVariationByIdQueryHandler> logger,
            IVariationRepository variationRepository, IMapper mapper)
        {
            _logger = logger;
            _variationRepository = variationRepository;
            _mapper = mapper;
        }

        public async Task<OneOf<ApiResponse<GetVariationOptionDto>, GetVariationOptionDto>> Handle(
            GetVariationByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Variation variation = await _variationRepository.GetByIdAsync(request.Id);
                if (variation == null)
                {
                    return ApiResponseBuilder.Error<GetVariationOptionDto>($"Không tìm thấy biến thể",
                        statusCode: 404);
                }
                GetVariationOptionDto dto = _mapper.Map<GetVariationOptionDto>(variation);
                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
