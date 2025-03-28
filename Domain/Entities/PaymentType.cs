using Domain.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PaymentType : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string? Value { get; set; }
        public ICollection<UserPayment>? Payments { get; set; }
    }
}
