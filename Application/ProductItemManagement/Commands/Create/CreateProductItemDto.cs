using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.ProductItemManagement.Commands.Create
{
    public class CreateProductItemDto
    {
        public string? SKU { get; set; }
        public string? Image { get; set; }
        public int? View_Count { get; set; }
        public string? Name { get; set; }
        public IFormFile? FromFileImages { get; set; }
        public decimal? Price { get; set; }
        public Guid? ProductId { get; set; }
        public ICollection<ProductConfigurationDto> Options { get; set; }
            = new List<ProductConfigurationDto>();
    }
    public class ProductConfigurationDto
    {
        public Guid? OptionId { get; set; }
    }
}
