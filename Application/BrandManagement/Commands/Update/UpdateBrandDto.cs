using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Application.BrandManagement.Commands.Update
{
    public class UpdateBrandDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        [BindNever]
        public string? Image { get; set; }

        public IFormFile formFile { get; set; }
        [JsonIgnore]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
