using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Application.ProductManagement.Commands.Update
{
    public class UpdateProductDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
        public decimal? Price { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SeriesId { get; set; }
        [JsonIgnore]
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
