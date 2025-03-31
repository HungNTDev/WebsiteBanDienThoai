using Microsoft.AspNetCore.Http;

namespace Application.CategoryManagement.Queries.GetDetail
{
    public class GetCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }

        public IFormFile? FromFileImages { get; set; }
    }
}
