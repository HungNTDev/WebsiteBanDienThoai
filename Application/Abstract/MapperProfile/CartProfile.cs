using Application.CartManagement.Queries.GetCartById;
using AutoMapper;
using Domain.Entities;

namespace Application.Abstract.MapperProfile
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<Cart, GetCartByIdDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));
        }
    }
}


