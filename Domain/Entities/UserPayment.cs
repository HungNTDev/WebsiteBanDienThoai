using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserPayment : BaseEntity
    {
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
