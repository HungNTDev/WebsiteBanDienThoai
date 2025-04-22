namespace Application.PaymentManagement.Commands.Create
{
    public class CreateVnPayPaymentUrlDto
    {
        public Guid OrderId { get; set; }
        public decimal OrderTotal { get; set; }
    }
}
