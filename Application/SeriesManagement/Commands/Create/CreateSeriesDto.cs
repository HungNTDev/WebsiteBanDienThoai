using System.Text.Json.Serialization;

namespace Application.SeriesManagement.Commands.Create
{
    public class CreateSeriesDto
    {
        public string? Name { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Guid BrandId { get; set; }

    }
}
