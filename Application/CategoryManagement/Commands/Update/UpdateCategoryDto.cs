using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Application.CategoryManagement.Commands.Update
{
    public class UpdateCategoryDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public IFormFile? FromFileImages { get; set; }
    }
}
