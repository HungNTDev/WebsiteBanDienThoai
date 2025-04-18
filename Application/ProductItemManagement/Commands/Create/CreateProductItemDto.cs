using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Application.ProductItemManagement.Commands.Create
{
    public class CreateProductItemDto
    {
        public string? SKU { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<ProductConfigurationDto> Options { get; set; }
            = new List<ProductConfigurationDto>();
    }
    public class ProductConfigurationDto
    {
        public Guid? OptionId { get; set; }
    }

    public class ProductItemDto
    {
        public Guid Id { get; set; }
        public string SKU { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Name { get; set; }
        public List<Guid> VariationOptions { get; set; } = new();
    }
}
