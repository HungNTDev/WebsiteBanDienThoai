using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.AuthenticationManagement.Commands.EditProfile
{
    public class EditProfileCommandHandler : ICommandHandler<EditProfileCommand, ApiResponse<object>>
    {
        private readonly ILogger<EditProfileCommandHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<EditProfileDto> _validator;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditProfileCommandHandler(ILogger<EditProfileCommandHandler> logger,
                                         UserManager<ApplicationUser> userManager,
                                         IValidator<EditProfileDto> validator,
                                         IUserRepository userRepository,
                                         IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _validator = validator;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<object>> Handle(EditProfileCommand request, CancellationToken cancellationToken)
        {
            var model = request.model;
            try
            {
                var user = await _userRepository.GetByIdAsync(model.Id);
                if (user == null)
                {
                    return ApiResponseBuilder.Error<object>("Không tìm thấy người dùng");
                }
                user.PhoneNumber = model.Phone;
                user.FullName = model.Name;
                user.Email = model.Email;
                user.Image = model.Image;

                await _unitOfWork.UserRepository.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponseBuilder.Success<object>(user, "Cập nhật Profile thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Lỗi");
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
