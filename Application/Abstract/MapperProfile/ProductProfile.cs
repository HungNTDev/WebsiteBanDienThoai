using Application.ProductManagement.Commands.Create;
using Application.ProductManagement.Commands.Update;
using Application.ProductManagement.Queries.GetAll;
using Application.ProductManagement.Queries.GetDetail;
using AutoMapper;
using Domain.Entities;

namespace Application.Abstract.MapperProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            Init();
        }
        private void Init()
        {
            CreateMap<Product, GetAllProductDto>();
            CreateMap<UpdateProductDto, Product>();
            //CreateMap<CreateProductDto, Product>();
            CreateMap<Product, GetDetailProductDto>()
            .ForMember(dest => dest.Variations, opt => opt.MapFrom(src =>
                src.ProductItems.SelectMany(pi => pi.ProductConfigs)
                    .GroupBy(pc => pc.VariationOption.Variation)
                    .Select(g => new Variation
                    {
                        Name = g.Key.Name,
                        VariationOption = g.Select(v => new VariationOption
                        {
                            Id = v.Id,
                            Value = v.VariationOption.Value
                        })
                          .DistinctBy(o => o.Value)
                          .ToList()
                    }).ToList()
            ));
            CreateMap<Variation, GetVariationDto>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.VariationOption));

            CreateMap<VariationOption, GetVariationOptionDto>()
                .ForMember(dest => dest.OptionId, opt => opt.MapFrom(src => src.Id))
                ;
        }
    }
}
