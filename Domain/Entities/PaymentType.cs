using Domain.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PaymentType : BaseEntity
    {
        public string? Value { get; set; }
        public ICollection<UserPayment>? Payments { get; set; }
    }
}
