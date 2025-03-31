using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class CartItem : BaseEntity
    {
        [ForeignKey("Cart")]
        public Guid CartId { get; set; }
        public Cart? Cart { get; set; }

        [ForeignKey("Combo")]
        public Guid ComboId { get; set; }
        public Combo? Combo { get; set; }

        [ForeignKey("ProductItem")]
        public Guid ProductItemId { get; set; }
        public ProductItem? ProductItem { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
