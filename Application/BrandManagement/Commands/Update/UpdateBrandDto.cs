using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Application.BrandManagement.Commands.Update
{
    public class UpdateBrandDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string? Image { get; set; }

        public IFormFile formFile { get; set; }
        [JsonIgnore]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
