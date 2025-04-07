using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using OneOf;

namespace Application.AuthenticationManagement.Queries.Profile
{
    public sealed class GetProfileQueryHandler : IQueryHandler<GetProfileQuery,
       OneOf<ApiResponse<GetProfileDto>, GetProfileDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;

        public GetProfileQueryHandler(UserManager<ApplicationUser> userManager,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<OneOf<ApiResponse<GetProfileDto>, GetProfileDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return new ApiResponse<GetProfileDto>
                {
                    StatusCode = 400,
                    Message = "Invalid request: User ID cannot be empty.",
                    Data = null
                };
            }

            try
            {
                var user = await _userRepository.GetByIdAsync(request.Id);

                if (user == null)
                {
                    return new ApiResponse<GetProfileDto>
                    {
                        StatusCode = 404,
                        Message = "User not found.",
                        Data = null
                    };
                }

                var dto = new GetProfileDto
                {
                    Name = user.UserName,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    Image = user.Image
                };

                return dto;
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetProfileDto>
                {
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}
