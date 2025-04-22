using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.Abstract.Repository.Base;
using Domain.Entities;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.AuthenticationManagement.Commands.Google
{
    public class GoogleLoginRequestCommandHandler
        : ICommandHandler<GoogleLoginRequestCommand, ApiResponse<object>>
    {
        private readonly ILogger<GoogleLoginRequestCommandHandler> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GoogleLoginRequestCommandHandler(IConfiguration configuration,
                                                ILogger<GoogleLoginRequestCommandHandler> logger,
                                                IUserRepository userRepository,
                                                IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _logger = logger;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<object>> Handle(GoogleLoginRequestCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(request.model.IdToken,
                   new GoogleJsonWebSignature.ValidationSettings
                   {
                       Audience = new[] { _configuration["Google:ClientId"] }
                   });

                if (payload == null)
                {
                    return ApiResponseBuilder.Error<object>("Invalid Google token");
                }

                var user = await _userRepository.GetByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        Email = payload.Email,
                        UserName = payload.Email,
                        FullName = payload.Name,
                        Image = payload.Picture,
                    };
                    await _userRepository.CreateAsync(user);
                    await _unitOfWork.SaveChangesAsync();
                }

                var token = GenerateToken(user);
                return ApiResponseBuilder.Success<object>(new
                {
                    Token = token,
                    User = new
                    {
                        user.Id,
                        user.FullName,
                        user.Email,
                        user.Image
                    }
                }, "Đăng nhập Google thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Lỗi");
                throw new NullReferenceException(nameof(Handle));
            }
        }

        private string GenerateToken(ApplicationUser user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FullName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
