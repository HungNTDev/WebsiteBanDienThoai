using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.CategoryManagement.Commands.Update
{
    public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand,
        ApiResponse<object>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateCategoryDto> _validator;
        private readonly ILogger<UpdateCategoryCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCategoryCommandHandler(ILogger<UpdateCategoryCommandHandler> logger,
                                            IValidator<UpdateCategoryDto> validator,
                                            IMapper mapper,
                                            ICategoryRepository categoryRepository,
                                            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<object>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var model = request.model;
            try
            {
                var validationResult = await _validator.ValidateAsync(model, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ApiResponseBuilder.ValidationError<object>(validationResult.Errors);
                }

                var category = await _categoryRepository.GetByIdAsync(model.Id);
                if (category == null)
                {
                    return ApiResponseBuilder.ValidationError<object>(validationResult.Errors);
                }

                var updateCategory = _mapper.Map(model, category);
                updateCategory.UpdatedBy = request.userName;
                _categoryRepository.Update(updateCategory);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>("", $"Cập nhật thể loại sản phẩm  thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Có lỗi rồi");
                throw;
            }
        }
    }
}
