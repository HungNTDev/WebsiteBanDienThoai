using System.Text.Json.Serialization;

namespace Application.VariationManagement.Commands.Update
{
    public class UpdateVariationDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
