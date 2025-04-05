using System.Text.Json.Serialization;

namespace Application.VariationManagement.Commands.Create
{
    public class CreateVariationDto
    {
        public string? Name { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
