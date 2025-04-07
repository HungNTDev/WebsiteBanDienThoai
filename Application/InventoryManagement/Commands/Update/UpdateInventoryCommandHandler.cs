using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.InventoryManagement.Commands.Update
{
    public class UpdateInventoryCommandHandler
        : ICommandHandler<UpdateInventoryCommand, ApiResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IValidator<UpdateInventoryDto> _validator;
        private readonly ILogger<UpdateInventoryCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateInventoryCommandHandler(ILogger<UpdateInventoryCommandHandler> logger,
                                             IValidator<UpdateInventoryDto> validator,
                                             IInventoryRepository inventoryRepository,
                                             IUnitOfWork unitOfWork,
                                             IMapper mapper)
        {
            _logger = logger;
            _validator = validator;
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<object>> Handle(UpdateInventoryCommand request,
            CancellationToken cancellationToken)
        {
            var model = request.model;
            try
            {
                var validationResult = _validator.Validate(model);
                if (!validationResult.IsValid)
                {
                    return ApiResponseBuilder.ValidationError<object>(validationResult.Errors);
                }

                var inventory = _inventoryRepository.GetByIdAsync(model.Id);
                if (inventory == null)
                {
                    return ApiResponseBuilder.Error<object>("Inventory not found", statusCode: 404);
                }
                var updatedInventory = _mapper.Map<Domain.Entities.Inventory>(model);
                updatedInventory.UpdatedBy = request.userName;
                _inventoryRepository.Update(updatedInventory);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>(inventory, "Inventory updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating inventory");
                return ApiResponseBuilder.Error<object>("An unexpected error occurred", statusCode: 500);
            }
            throw new NotImplementedException();
        }
    }
}
