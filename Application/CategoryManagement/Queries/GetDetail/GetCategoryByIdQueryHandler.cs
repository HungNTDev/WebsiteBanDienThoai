using Application.Abstract;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Application.CategoryManagement.Queries.GetDetail
{
    public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery,
        OneOf<ApiResponse<GetCategoryDto>, GetCategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetCategoryByIdQueryHandler> _logger;

        public GetCategoryByIdQueryHandler(ILogger<GetCategoryByIdQueryHandler> logger,
                                           ICategoryRepository categoryRepository,
                                           IMapper mapper)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<OneOf<ApiResponse<GetCategoryDto>, GetCategoryDto>> Handle(GetCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                Category category = await _categoryRepository.GetByIdAsync(request.id);
                if (category == null)
                {
                    return ApiResponseBuilder.Error<GetCategoryDto>($"Không tìm thấy thể loại ",
                        statusCode: 404);
                }
                GetCategoryDto dto = _mapper.Map<GetCategoryDto>(category);
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
