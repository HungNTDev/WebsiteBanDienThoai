using Application.AuthenticationManagement.Commands.Register;
using AutoMapper;
using Domain.Entities;

namespace Application.Abstract.MapperProfile
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<RegisterModel, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
