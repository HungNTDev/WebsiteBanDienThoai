using Application.Abstract;
using Application.Abstract.CQRS;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.AuthenticationManagement.Commands.Login
{
    public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, ApiResponse<object>>
    {
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly IValidator<LoginModel> _validator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public LoginCommandHandler(IValidator<LoginModel> validator,
                                   ILogger<LoginCommandHandler> logger,
                                   IConfiguration configuration,
                                   UserManager<ApplicationUser> userManager,
                                   SignInManager<ApplicationUser> signInManager)
        {
            _validator = validator;
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApiResponse<object>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var model = request.model;
            try
            {
                var valiationResult = await _validator.ValidateAsync(model, cancellationToken);
                if (!valiationResult.IsValid)
                {
                    return ApiResponseBuilder.ValidationError<object>(valiationResult.Errors);
                }

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return ApiResponseBuilder.ValidationError<object>(valiationResult.Errors);
                }

                var result = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!result)
                {
                    return ApiResponseBuilder.ValidationError<object>(valiationResult.Errors);
                }

                var claims = new List<Claim>
                {
                     new Claim("Id", user.Id.ToString()),
                     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                     new Claim(ClaimTypes.Email, user.Email),
                     new Claim(ClaimTypes.Name, user.FullName)
                };

                var roles = await _signInManager.UserManager.GetRolesAsync(user);

                if (roles.Any())
                {
                    var roleClaim = string.Join(",", roles);
                    claims.Add(new Claim(ClaimTypes.Role, roleClaim));
                }

                var token = CreateToken(claims);
                var refreshToken = GenerateRefreshToken();

                return ApiResponseBuilder.Success<object>(new JwtSecurityTokenHandler().WriteToken(token),
                    "Đăng nhập thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Lỗi");
                throw new NullReferenceException(nameof(Handle));
            }
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private JwtSecurityToken CreateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));
            var add30Date = DateTime.Now.AddDays(30);

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: add30Date.ToUniversalTime(),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
