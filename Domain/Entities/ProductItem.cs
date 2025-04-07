using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ProductItem : BaseEntity
    {
        public string? Stock { get; set; }
        public string? SKU { get; set; }
        public string? Image { get; set; }
        public int? View_Count { get; set; }
        public string? Name { get; set; }
        [NotMapped]
        public IFormFile? FromFileImages { get; set; }
        public decimal? Price { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }


        public Guid InventoryId { get; set; }
        public Inventory? Inventory { get; set; }

        public ICollection<ProductConfig>? ProductConfigs { get; set; }
        public ICollection<ComboProduct>? ComboProducts { get; set; }
    }
}
