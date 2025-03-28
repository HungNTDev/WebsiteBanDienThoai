using Application.Abstract;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.CategoryManagement.Commands.Create
{
    public class CreateCategoryCommandHandler
        : ICommandHandler<CreateCategoryCommand, ApiResponse<object>>
    {
        private readonly ILogger<CreateCategoryCommandHandler> _logger;
        private readonly IValidator<CreateCategoryDto> _validator;
        private readonly ICategoryRepository _categoryRepostiory;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork,
                                            ICategoryRepository categoryRepostiory,
                                            IValidator<CreateCategoryDto> validator,
                                            ILogger<CreateCategoryCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _categoryRepostiory = categoryRepostiory;
            _validator = validator;
            _logger = logger;
        }

        public async Task<ApiResponse<object>> Handle(CreateCategoryCommand request,
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

                var categoryExisted = await _categoryRepostiory
                    .IsCategoryExistsAsync(model.Name, cancellationToken);
                if (categoryExisted)
                {
                    return ApiResponseBuilder.Error<object>($"" +
                        $"Thể loại {model.Name} đã tồn tại");
                }

                var categoryDto = new Category
                {
                    Name = model.Name,
                    Image = model.Image,
                };
                _categoryRepostiory.Create(categoryDto);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>("",
                    $"Thêm thể loại sản phẩm {model.Name} thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Có lỗi rồi");
                throw;
            }
        }
    }
}
