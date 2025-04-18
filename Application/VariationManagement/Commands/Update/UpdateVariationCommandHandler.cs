using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.VariationManagement.Commands.Update
{
    public class UpdateVariationCommandHandler
        : ICommandHandler<UpdateVariationCommand, ApiResponse<object>>
    {
        private readonly IVariationRepository _variationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateVariationDto> _validator;
        private readonly ILogger<UpdateVariationCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateVariationCommandHandler(IMapper mapper,
            ILogger<UpdateVariationCommandHandler> logger,
            IValidator<UpdateVariationDto> validator,
            IUnitOfWork unitOfWork, IVariationRepository variationRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _variationRepository = variationRepository;
        }

        public async Task<ApiResponse<object>> Handle(UpdateVariationCommand request,
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

                var variation = await _variationRepository.GetByIdAsync(model.Id);
                if (variation == null)
                {
                    return ApiResponseBuilder.Error<object>("Không tìm thấy biến thể", statusCode: 404);
                }

                var updateVariation = _mapper.Map(model, variation);
                updateVariation.UpdatedBy = request.userName;
                _variationRepository.Update(variation);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>("", $"Cập nhật biến thể sản phẩm  thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Có lỗi rồi");
                throw;
            }
        }
    }
}
