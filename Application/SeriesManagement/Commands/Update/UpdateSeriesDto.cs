using System.Text.Json.Serialization;

namespace Application.SeriesManagement.Commands.Update
{
    public class UpdateSeriesDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public Guid BrandId { get; set; }
    }
}
