using Application.InventoryManagement.Commands.Update;
using Application.InventoryManagement.Queries.GetAll;
using Application.InventoryManagement.Queries.GetDetail;
using AutoMapper;
using Domain.Entities;

namespace Application.Abstract.MapperProfile
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            Init();
        }
        private void Init()
        {
            CreateMap<UpdateInventoryDto, Inventory>();
            CreateMap<Inventory, GetAll_InventoryDto>();
            CreateMap<Inventory, GetDetail_InventoryDto>();
        }
    }
}
