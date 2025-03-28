using Domain.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class VariationOption : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string? Value { get; set; }

        [ForeignKey("Variation")]
        public Guid VariationId { get; set; }
        public Variation? Variation { get; set; }

        public ICollection<ProductConfig>? ProductConfigs { get; set; }
    }
}
