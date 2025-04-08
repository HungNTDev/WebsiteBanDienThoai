using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Application.BrandManagement.Commands.Create
{
    public class CreateBrandDto
    {
        public string? Name { get; set; }

        [JsonIgnore]

        public string? Image { get; set; }

        public IFormFile formFile { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
