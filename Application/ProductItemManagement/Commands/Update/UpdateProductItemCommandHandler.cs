using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.ProductItemManagement.Commands.Update
{
    public class UpdateProductItemCommandHandler : ICommandHandler<UpdateProductItemCommand, ApiResponse<object>>
    {
        private readonly IProductItemRepository _productItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateProductItemCommandHandler> _logger;
        private readonly IValidator<UpdateProductItemDto> _validator;

        public UpdateProductItemCommandHandler(
            IProductItemRepository productItemRepository,
            IUnitOfWork unitOfWork,
            ILogger<UpdateProductItemCommandHandler> logger,
            IValidator<UpdateProductItemDto> validator)
        {
            _productItemRepository = productItemRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _validator = validator;
        }
        public async Task<ApiResponse<object>> Handle(UpdateProductItemCommand request, CancellationToken cancellationToken)
        {
            var model = request.model;
            try
            {
                var validationResult = _validator.ValidateAsync(model, cancellationToken);
                if (!validationResult.Result.IsValid)
                {
                    return ApiResponseBuilder.ValidationError<object>(validationResult.Result.Errors);
                }

                var productItem = await _productItemRepository.GetByIdAsync(model.Id);
                if (productItem == null)
                {
                    return ApiResponseBuilder.Error<object>("Product item not found", statusCode: 404);
                }


                var productItemToUpdate = new ProductItem
                {
                    SKU = model.SKU,
                    Name = model.Name,
                    Price = model.Price,
                    Image = model.Image,
                    ProductId = model.ProductId,
                    UpdatedBy = request.userName,
                };
                var optionIds = model.Options.Select(o => o.OptionId ?? Guid.Empty).ToList();
                await _productItemRepository.UpdateAsync(model.Id, productItemToUpdate, optionIds);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>("", $"Cập nhật sản phẩm {model.SKU} thành công",
                     statusCode: 201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Create Product Item");
                return ApiResponseBuilder.Error<object>("An unexpected error occurred", statusCode: 500);
            }
        }
    }
}
