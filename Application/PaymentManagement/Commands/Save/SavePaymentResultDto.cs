using Domain.Enum;

namespace Application.PaymentManagement.Commands.Save
{
    public class SavePaymentResultDto
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public string? Provider { get; set; }
        public string? TransactionId { get; set; }
        public decimal? Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public bool IsSuccess { get; set; }
    }
}
