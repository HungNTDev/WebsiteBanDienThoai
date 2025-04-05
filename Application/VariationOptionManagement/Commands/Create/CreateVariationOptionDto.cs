using System.Text.Json.Serialization;

namespace Application.VariationOptionManagement.Commands.Create
{
    public class CreateVariationOptionDto
    {
        public string? Name { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Guid VariationId { get; set; }
        public string? VariationName { get; set; }
    }
}
