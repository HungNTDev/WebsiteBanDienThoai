using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.CategoryManagement.Queries.GetDetail;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Application.InventoryManagement.Queries.GetDetail
{
    public sealed class GetDetail_InventoryQueryHandler :
        IQueryHandler<GetDetail_InventoryQuery, OneOf<ApiResponse<GetDetail_InventoryDto>, GetDetail_InventoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ILogger<GetDetail_InventoryQueryHandler> _logger;

        public GetDetail_InventoryQueryHandler(ILogger<GetDetail_InventoryQueryHandler> logger, IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _logger = logger;
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }

        public async Task<OneOf<ApiResponse<GetDetail_InventoryDto>, GetDetail_InventoryDto>> Handle(GetDetail_InventoryQuery request, CancellationToken cancellationToken)
        {
            var model = request.id;
            try
            {
                Inventory inventory = await _inventoryRepository.GetByIdAsync(model);
                if (inventory == null)
                {
                    return ApiResponseBuilder.Error<GetDetail_InventoryDto>($"Không tìm thấy thể loại ",
                       statusCode: 404);
                }

                GetDetail_InventoryDto dto = _mapper.Map<GetDetail_InventoryDto>(inventory);
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
