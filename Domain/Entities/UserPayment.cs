using Domain.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserPayment : IEntity
    {
        [Key]
        public Guid Id { get; set; }        
        public string? Provider { get; set; }

        [ForeignKey("ApplicationUser")]
        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [ForeignKey("PaymentType")]
        public Guid PaymentTypeId { get; set; }
        public PaymentType? PaymentType { get; set; }

        public ICollection<Payment>? Payments { get; set; }
    }
}
