using Application.VariationOptionManagement.Commands.Update;
using Application.VariationOptionManagement.Queries.GetAll;
using Application.VariationOptionManagement.Queries.GetById;
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
            CreateMap<VariationOption, GetVariationOptionByIdDto>();
            CreateMap<UpdateVariationOptionDto, VariationOption>();
        }
    }
}
