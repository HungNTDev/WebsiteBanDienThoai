using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.AuthenticationManagement.Commands.Register
{
    public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, ApiResponse<object>>
    {
        private readonly IValidator<RegisterModel> _validatorRegister;
        private readonly ILogger<RegisterCommandHandler> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        private IConfiguration _configuration;
        private IMapper _mapper;

        public RegisterCommandHandler(IMapper mapper,
                                      IConfiguration configuration,
                                      RoleManager<ApplicationRole> roleManager,
                                      UserManager<ApplicationUser> userManager,
                                      SignInManager<ApplicationUser> signInManager,
                                      ILogger<RegisterCommandHandler> logger,
                                      IValidator<RegisterModel> validatorRegister)
        {
            _mapper = mapper;
            _configuration = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _validatorRegister = validatorRegister;
        }

        public async Task<ApiResponse<object>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var model = request.model;
            var valiationResult = await _validatorRegister.ValidateAsync(model);
            if (!valiationResult.IsValid)
            {
                return ApiResponseBuilder.ValidationError<object>(valiationResult.Errors);
            }
            try
            {
                var isMailExisted = await _userManager.FindByEmailAsync(model.Email);
                if (isMailExisted != null)
                {
                    return ApiResponseBuilder.ValidationError<object>(valiationResult.Errors);
                }

                var user = _mapper.Map<ApplicationUser>(model);
                user.UserName = model.Email;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roles = await _signInManager.UserManager.GetRolesAsync(user);
                    if (!roles.Any())
                    {
                        await _signInManager.UserManager.AddToRoleAsync(user, "User");
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return ApiResponseBuilder.Success<object>("", "Đăng ký thành công");
                }
                return ApiResponseBuilder.ValidationError<object>(valiationResult.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
