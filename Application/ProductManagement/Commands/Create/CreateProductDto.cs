using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Application.ProductManagement.Commands.Create
{
    public class CreateProductDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
        public decimal? Price { get; set; }
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public Guid SeriesId { get; set; }
        public string? SeriesName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
