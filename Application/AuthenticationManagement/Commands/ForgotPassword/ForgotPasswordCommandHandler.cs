using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Services;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Application.AuthenticationManagement.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand, ApiResponse<object>>
    {
        private readonly IValidator<ForgotPasswordModel> _validator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ForgotPasswordCommandHandler> _logger;
        private readonly IEmailService _emailService;

        public ForgotPasswordCommandHandler(ILogger<ForgotPasswordCommandHandler> logger,
                                            UserManager<ApplicationUser> userManager,
                                            IValidator<ForgotPasswordModel> validator,
                                            IEmailService emailService)
        {
            _logger = logger;
            _userManager = userManager;
            _validator = validator;
            _emailService = emailService;
        }

        public async Task<ApiResponse<object>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            try
            {
                var validationResult = await _validator.ValidateAsync(model, cancellationToken);

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return ApiResponseBuilder.ValidationError<object>(validationResult.Errors);
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = HttpUtility.UrlEncode(token);

                var role = await _userManager.GetRolesAsync(user);
                var userRole = role.FirstOrDefault();

                string frontEndDomain = userRole == "Admin"
                    ? "https://localhost:7210"
                    : "https://localhost:7001";

                var resetLink = $"{frontEndDomain}/resetpassword?email={model.Email}&token={encodedToken}";

                var subject = $"Tạo lại mật khẩu cho {user.UserName}";
                var body = $@"
                     Chào {user.UserName},<br/><br/>
                     Dưới đây là link để bạn đặt lại mật khẩu:
                     <a href=""{resetLink}""> Nhấn vào đây để đặt lại mật khẩu </a><br/><br/>
                     Nếu có thắc mắc, vui lòng liên hệ bộ phận hỗ trợ.<br/><br/>
                     Trân trọng,<br/>
                     Đội ngũ hỗ trợ cellphoneS
                 ";

                await _emailService.SendAsync(user.Email, subject, body);

                return ApiResponseBuilder.Success<object>(null, "Email đặt lại mật khẩu đã được gửi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi trong Handle ForgotPasswordCommand");
                throw;
            }
        }
    }
}
