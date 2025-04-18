using Application.OrderManagement.Commands.Create;

namespace Application.OrderManagement.Queries.GetOrderById
{
    public class GetOrderByIdDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public string Status { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public List<PaymentDto> Payments { get; set; }
    }
    public class PaymentDto
    {
        public string Provider { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
