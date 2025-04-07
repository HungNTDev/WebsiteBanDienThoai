using Application.BrandManagement.Commands.Update;
using Application.BrandManagement.Queries.GetAll;
using Application.BrandManagement.Queries.GetDetail;
using AutoMapper;
using Domain.Entities;

namespace Application.Abstract.MapperProfile
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            Init();
        }
        private void Init()
        {
            CreateMap<Brand, GetAllBrandDto>();
            CreateMap<Brand, GetDetailBrandDto>();
            CreateMap<UpdateBrandDto, Brand>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
