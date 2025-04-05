using Application.Abstract;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.VariationOptionManagement.Commands.Create
{
    public sealed class CreateVariationOptionCommandHandler : ICommandHandler<CreateVariationOptionCommand,
        ApiResponse<object>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateVariationOptionCommandHandler> _logger;
        private readonly IValidator<CreateVariationOptionDto> _validator;

        public CreateVariationOptionCommandHandler(ILogger<CreateVariationOptionCommandHandler> logger,
            IUnitOfWork unitOfWork,
            IValidator<CreateVariationOptionDto> validator)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ApiResponse<object>> Handle(CreateVariationOptionCommand request,
            CancellationToken cancellationToken)
        {
            var model = request.model;
            try
            {
                var validationResult = await _validator.ValidateAsync(model,
                  cancellationToken);

                if (!validationResult.IsValid)
                {
                    return ApiResponseBuilder.ValidationError<object>(
                        validationResult.Errors);
                }
                var variationOption = new VariationOption
                {
                    Value = model.Name,
                    VariationId = model.VariationId,

                    CreatedBy = request.userName
                };
                await _unitOfWork.VariationOption.CreateAsync(variationOption);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>("",
                    $"Thêm mới tùy chọn biến thể sản phẩm thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating variation option for VariationId: {VariationId}", model.VariationId);
                throw;  // Re-throwing the exception after logging it.
            }
        }
    }
}
