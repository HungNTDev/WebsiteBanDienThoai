using Domain.Abstract;
using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }

        [ForeignKey("ApplicationUser")]
        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [ForeignKey("Address")]
        public Guid AddressId { get; set; }
        public Address? Address { get; set; }

        [ForeignKey("ShippingMethod")]
        public Guid? ShippingMethodId { get; set; }
        public ShippingMethod? ShippingMethod { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<UserPayment>? Payments { get; set; }
        public OrderStatus Status { get; set; }
    }
}
