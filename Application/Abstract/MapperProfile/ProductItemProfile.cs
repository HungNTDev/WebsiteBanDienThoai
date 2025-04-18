using Application.ProductItemManagement.Commands.Create;
using Application.ProductItemManagement.Queries.GetAll;
using Application.ProductItemManagement.Queries.GetByOptions;
using AutoMapper;
using Domain.Entities;

namespace Application.Abstract.MapperProfile
{
    public class ProductItemProfile : Profile
    {
        public ProductItemProfile()
        {
            Init();
        }
        private void Init()
        {
            CreateMap<CreateProductItemDto, ProductItem>()
                .ForMember(x => x.ProductConfigs, y => y.MapFrom(z => z.Options));

            CreateMap<ProductConfigurationDto, ProductConfig>()
                //.ForMember(x => x.Id, y => y.MapFrom(z => z.))
                .ForMember(x => x.VariationOptionId, y => y.MapFrom(z => z.OptionId))
                .ForMember(x => x.ProductItemId, y => y.Ignore());

            CreateMap<ProductItem, GetProductItemDto>()
            .ForMember(dest => dest.Variations, opt => opt.MapFrom(src =>
                src.ProductConfigs
                    .GroupBy(pc => pc.VariationOption.Variation)
                    .Select(g => new VariationDto
                    {
                        Name = g.Key.Name,
                        Options = g.Select(v => new VariationOptionDto { Value = v.VariationOption.Value })
                                   .DistinctBy(o => o.Value)
                                   .ToList()
                    })
                    .ToList()
            ));

            CreateMap<VariationOption, VariationOptionDto>();
            CreateMap<ProductItem, GetAllProductItemDto>();
        }
    }
}
