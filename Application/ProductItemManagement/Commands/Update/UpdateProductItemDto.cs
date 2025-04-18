using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Application.ProductItemManagement.Commands.Update
{
    public class UpdateProductItemDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? SKU { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        [FromForm(Name = "Options")]
        public ICollection<UpdateProductConfigurationDto> Options { get; set; }
            = new List<UpdateProductConfigurationDto>();
    }
    public class UpdateProductConfigurationDto
    {
        public Guid? OptionId { get; set; }
    }
}
