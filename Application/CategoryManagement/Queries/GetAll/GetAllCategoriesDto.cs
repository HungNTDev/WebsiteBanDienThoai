using Microsoft.AspNetCore.Http;

namespace Application.CategoryManagement.Queries.GetAll
{
    public class GetAllCategoriesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}
