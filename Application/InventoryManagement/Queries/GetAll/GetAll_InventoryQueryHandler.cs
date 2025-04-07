using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.ProductManagement.Queries.GetAll;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.InventoryManagement.Queries.GetAll
{
    public class GetAll_InventoryQueryHandler
        : IQueryHandler<GetAll_InventoryQuery, ApiResponse<PaginatedResult<GetAll_InventoryDto>>>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAll_InventoryQueryHandler> _logger;

        public GetAll_InventoryQueryHandler(ILogger<GetAll_InventoryQueryHandler> logger,
                                            IMapper mapper,
                                            IInventoryRepository inventoryRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<ApiResponse<PaginatedResult<GetAll_InventoryDto>>> Handle(GetAll_InventoryQuery request, CancellationToken cancellationToken)
        {
            var model = request.filter;
            try
            {
                var inventories = await _inventoryRepository.GetAllAsync();

                var searchString = model.SearchTerm.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    _logger.LogInformation("Tìm kiếm với chuỗi: {SearchString}", searchString);
                    var normalizedSearch = searchString.Trim().ToLower();

                    inventories = inventories.Where(x =>
                        x.ProductItem != null && x.ProductItem.Name.ToLower().Contains(normalizedSearch)
                    ).AsQueryable();
                }

                if (inventories == null || !inventories.Any())
                {
                    return ApiResponseBuilder.Error<PaginatedResult<GetAll_InventoryDto>>
                       ($"Không tìm thấy sản phẩm {searchString}", statusCode: 404);
                }

                var inventoriesPaginated = PaginatedResult<Inventory>.CreateAsync
                    (inventories.AsQueryable(), model.PageIndex, model.PageSize, cancellationToken).Result;

                var inventoriesDto = _mapper.Map<List<GetAll_InventoryDto>>
                    (inventoriesPaginated.Items);

                var paginatedResult = new PaginatedResult<GetAll_InventoryDto>
                (inventoriesDto,
                    inventoriesPaginated.PageSize,
                    inventoriesPaginated.PageIndex,
                    inventoriesPaginated.TotalCount);
                return ApiResponseBuilder.Success(paginatedResult, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Lỗi");
                throw;
            }
        }
    }
}
