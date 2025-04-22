namespace Domain.Entities
{
    public class PaymentType : BaseEntity
    {
        public string? Value { get; set; }
        public string? Code { get; set; }
        public ICollection<UserPayment>? Payments { get; set; }
    }
}
