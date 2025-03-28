using Domain.Abstract;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Category : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? FromFileImages { get; set; }
        public ICollection<Product>? Products { get; set; }
        public ICollection<Variation>? Variations { get; set; }
        public ICollection<PromotionCategory>? PromotionCategories { get; set; }
    }
}
