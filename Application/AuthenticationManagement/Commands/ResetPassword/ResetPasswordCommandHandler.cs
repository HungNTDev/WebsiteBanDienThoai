using Application.Abstract;
using Application.Abstract.CQRS;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.AuthenticationManagement.Commands.ResetPassword
{
    public sealed class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, ApiResponse<object>>
    {
        private readonly ILogger<ResetPasswordCommandHandler> _logger;
        private readonly IValidator<ResetPasswordModels> _validator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ResetPasswordCommandHandler(IMapper mapper, UserManager<ApplicationUser> userManager,
                                           IValidator<ResetPasswordModels> validator,
                                           ILogger<ResetPasswordCommandHandler> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _validator = validator;
            _logger = logger;
        }

        public async Task<ApiResponse<object>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var model  = request.Model;
            try
            {
                var validationResult = await _validator.ValidateAsync(model, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ApiResponseBuilder.ValidationError<object>(validationResult.Errors);
                }

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null) 
                {
                    return ApiResponseBuilder.ValidationError<object>(validationResult.Errors);
                }

                var tokenValid = await _userManager.VerifyUserTokenAsync(user,
                 TokenOptions.DefaultProvider, "ResetPassword", model.Token);

                if (!tokenValid)
                {
                    return ApiResponseBuilder.ValidationError<object>(validationResult.Errors);
                }

                var resetResult = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                if (!resetResult.Succeeded)
                {
                    return ApiResponseBuilder.ValidationError<object>(validationResult.Errors);
                }
                return ApiResponseBuilder.Success<object>("", "Đặt mật khẩu lại thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Lỗi");
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
