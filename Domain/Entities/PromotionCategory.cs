using Domain.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PromotionCategory : IEntity
    {
        [Key]
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        [Key]
        [ForeignKey("Promotion")]
        public Guid PromotionId { get; set; }
        public Promotion? Promotion { get; set; }
    }
}
