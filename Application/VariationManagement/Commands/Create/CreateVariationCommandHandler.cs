using Application.Abstract;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.VariationManagement.Commands.Create
{
    public class CreateVariationCommandHandler
        : ICommandHandler<CreateVariationCommand, ApiResponse<object>>
    {
        private readonly IVariationRepository _variationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateVariationDto> _validator;
        private readonly ILogger<CreateVariationCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateVariationCommandHandler(
            ILogger<CreateVariationCommandHandler> logger,
            IValidator<CreateVariationDto> validator,
            IUnitOfWork unitOfWork,
            IVariationRepository variationRepository,
            IMapper mapper)
        {
            _logger = logger;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _variationRepository = variationRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<object>> Handle(CreateVariationCommand request,
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

                var variation = new Variation
                {
                    Name = model.Name,
                    CreatedDate = model.CreatedDate,
                    CreatedBy = request.userName,
                    CategoryId = model.CategoryId,
                };
                await _unitOfWork.Variation.CreateAsync(variation);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>("",
                    $"Thêm biến thể {model.Name} thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Có lỗi rồi");
                throw;
            }
        }
    }
}
