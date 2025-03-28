using Domain.Abstract;
using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Payment : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string? TransactionId { get; set; }
        public decimal? Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }

        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        [ForeignKey("UserPayment")]
        public Guid UserPaymentId { get; set; }
        public UserPayment? UserPayment { get; set; }
    }
}
