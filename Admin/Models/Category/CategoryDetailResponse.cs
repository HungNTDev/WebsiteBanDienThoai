using Microsoft.AspNetCore.Http;

namespace Admin.Models.Category
{
    public class CategoryDetailResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public IFormFile? FromFileImages { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}