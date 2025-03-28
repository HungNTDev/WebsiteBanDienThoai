using Domain.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class OrderItem : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        [ForeignKey("ProductItem")]
        public Guid ProductItemId { get; set; }
        public ProductItem? ProductItem { get; set; }

        [ForeignKey("Combo")]
        public Guid ComboId { get; set; }
        public Combo? Combo { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}

