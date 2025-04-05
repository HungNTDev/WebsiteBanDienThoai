using Application.VariationManagement.Commands.Create;
using Application.VariationManagement.Commands.Update;
using Application.VariationManagement.Queries.GetAll;
using Application.VariationManagement.Queries.GetById;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Domain.Entities;

namespace Application.MapperProfile
{
    public class VariationProfile : Profile
    {
        public VariationProfile()
        {
            Init();
        }
        private void Init()
        {
            CreateMap<Variation, GetAllVariationDto>();
            CreateMap<Variation, CreateVariationDto>()
                .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<UpdateVariationDto, Variation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<GetVariationOptionDto, Variation>();
        }
    }
}
