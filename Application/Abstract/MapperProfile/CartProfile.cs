using Application.CartManagement.Queries.GetCarByUser;
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
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName)) // hoặc User.UserName tùy bạn
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));

            CreateMap<CartItem, GetCartItem>()
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => Convert.ToDouble(src.UnitPrice)))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => Convert.ToDouble(src.TotalPrice)))
                .ForMember(dest => dest.ProductItemName, opt => opt.MapFrom(src => src.ProductItem.Name))
                .ForMember(dest => dest.ProductItemImage, opt => opt.MapFrom(src => src.Image));

            CreateMap<Cart, GetCartByUserDto>()
          .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
          .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));

            CreateMap<CartItem, GetCartItemByUserDto>()
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => Convert.ToDouble(src.UnitPrice)))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => Convert.ToDouble(src.TotalPrice)))
                .ForMember(dest => dest.ProductItemImage, opt => opt.MapFrom(src => src.ProductItem.Image))
                .ForMember(dest => dest.ProductItemName, opt => opt.MapFrom(src => src.ProductItem.Name));
        }
    }
}


