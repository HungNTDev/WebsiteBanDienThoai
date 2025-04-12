using System.Text.Json.Serialization;

namespace Application.VariationOptionManagement.Commands.Create
{
    public class CreateVariationOptionDto
    {
        public string? Value { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Guid VariationId { get; set; }
    }
}
