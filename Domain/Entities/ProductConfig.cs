using Domain.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ProductConfig : IEntity
    {
        [Key]
        [ForeignKey("ProductItem")]
        public Guid ProductItemId { get; set; }
        public ProductItem? ProductItem { get; set; }

        [Key]
        [ForeignKey("VariationOption")]
        public Guid VariationOptionId { get; set; }
        public VariationOption? VariationOption { get; set; }
    }
}
