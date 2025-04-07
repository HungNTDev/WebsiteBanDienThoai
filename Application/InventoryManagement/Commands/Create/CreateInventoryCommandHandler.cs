using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.InventoryManagement.Commands.Create
{
    public sealed class CreateInventoryCommandHandler
        : ICommandHandler<CreateInventoryCommand, ApiResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IValidator<CreateInventoryDto> _validator;
        private readonly ILogger<CreateInventoryCommandHandler> _logger;

        public CreateInventoryCommandHandler(ILogger<CreateInventoryCommandHandler> logger,
                                             IValidator<CreateInventoryDto> validator,
                                             IInventoryRepository inventoryRepository,
                                             IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _validator = validator;
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<object>> Handle(CreateInventoryCommand request,
            CancellationToken cancellationToken)
        {
            var model = request.model;
            try
            {
                var validationResult = await _validator.ValidateAsync(model, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ApiResponseBuilder.ValidationError<object>(validationResult.Errors);
                }
                var isInventoryExists = await _inventoryRepository
                    .IsInventoryExistsAsync(model.ProductItemId, cancellationToken);
                if (isInventoryExists)
                {
                    return ApiResponseBuilder.Error<object>("Inventory already exists", statusCode: 409);
                }
                var inventory = new Domain.Entities.Inventory
                {
                    ProductItemId = model.ProductItemId,
                    StockQuantity = model.StockQuantity,
                    CreatedBy = request.userName,
                };
                await _inventoryRepository.CreateAsync(inventory);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>(inventory, "Inventory created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while create inventory");
                return ApiResponseBuilder.Error<object>("An unexpected error occurred", statusCode: 500);
            }
        }
    }
}
