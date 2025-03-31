using Domain.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Promotion : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DiscountRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<PromotionCategory>? PromotionCategories { get; set; }
    }
}
