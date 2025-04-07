using Application.CategoryManagement.Commands.Update;
using Application.CategoryManagement.Queries.GetAll;
using Application.CategoryManagement.Queries.GetDetail;
using AutoMapper;
using Domain.Entities;

namespace Application.Abstract.MapperProfile
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            Init();
        }
        private void Init()
        {
            CreateMap<Category, GetAllCategoriesDto>();
            CreateMap<Category, GetCategoryDto>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
