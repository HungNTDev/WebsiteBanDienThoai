using Application.ProductManagement.Queries.GetDetail;
using Application.VariationManagement.Queries.GetById;
using Application.VariationOptionManagement.Commands.Update;
using Application.VariationOptionManagement.Queries.GetAll;
using AutoMapper;
using Domain.Entities;

namespace Application.Abstract.MapperProfile
{
    public class VariationOptionProfile : Profile
    {
        public VariationOptionProfile()
        {
            Init();
        }
        private void Init()
        {
            CreateMap<VariationOption, GetAllVariationOptionDto>();
            CreateMap<VariationOption, GetVariationOptionDto>();
            CreateMap<UpdateVariationOptionDto, VariationOption>();
        }
    }
}
