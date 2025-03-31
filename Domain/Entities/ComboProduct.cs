using Domain.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ComboProduct : BaseEntity
    {
        public int Quantity { get; set; }

        [ForeignKey("Combo")]
        public Guid ComboId { get; set; }
        public Combo? Combo { get; set; }

        [ForeignKey("ProductItem")]
        public Guid ProductItemId { get; set; }
        public ProductItem? ProductItem { get; set; }
    }
}
