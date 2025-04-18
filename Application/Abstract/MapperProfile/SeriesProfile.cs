using Application.SeriesManagement.Commands.Update;
using Application.SeriesManagement.Queries.GetAll;
using Application.SeriesManagement.Queries.GetDetail;
using AutoMapper;
using Domain.Entities;

namespace Application.Abstract.MapperProfile
{
    public class SeriesProfile : Profile
    {
        public SeriesProfile()
        {
            Init();
        }
        private void Init()
        {
            CreateMap<Series, GetAllSeriesDto>();
            CreateMap<Series, GetDetailSeriesDto>()
            .ForMember(dest => dest.BrandName,
                opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : null));
            CreateMap<UpdateSeriesDto, Series>();
        }
    }
}
