using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Brand : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile formFile { get; set; } = default!;
        public ICollection<CategoryBrand> CategoryBrands { get; set; }
        public ICollection<Series> Series { get; set; }
    }
}
