using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
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

        public ForgotPasswordCommandHandler(ILogger<ForgotPasswordCommandHandler> logger,
                                            UserManager<ApplicationUser> userManager,
                                            IValidator<ForgotPasswordModel> validator)
        {
            _logger = logger;
            _userManager = userManager;
            _validator = validator;
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
                    ? "https://localhost:7210" : "https://localhost:7106";

                var resetLink = $"{frontEndDomain}/resetpassword?email={model.Email}&token={encodedToken}";

                using (var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525))
                {
                    client.Credentials = new NetworkCredential("a62a9b76f3f09f", "29b27b34801af6");
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("hungntps38090@gmail.com"),
                        Subject = "Password Reset",
                        Body = $"Vui lòng truy cập link: {resetLink}",
                        IsBodyHtml = false
                    };
                    mailMessage.To.Add(user.Email);
                    await client.SendMailAsync(mailMessage);
                }
                return ApiResponseBuilder.Success<object>(null, "Email đặt lại mật khẩu đã được gửi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Lỗi");
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
