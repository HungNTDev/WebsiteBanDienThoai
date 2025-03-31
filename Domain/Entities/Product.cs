using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? FromFileImages { get; set; }
        public decimal? Price { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }
        public Brand? Brand { get; set; }

        public ICollection<ProductItem>? ProductItems { get; set; }
    }
}
