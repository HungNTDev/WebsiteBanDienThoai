namespace User.Models.Order
{
    public class CreateOrderCommand
    {
        public Guid UserId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public decimal OrderTotal { get; set; }
        public Guid PaymentTypeId { get; set; }
        //public List<PaymentDto> Payments { get; set; } = new();
    }

    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductItemId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

    }

    public class PaymentDto
    {
        public string? Provider { get; set; }
        public decimal? Amount { get; set; }
        public PaymentStatus Status { get; set; }
    }

    public enum PaymentStatus
    {
        Pending = 0,
        Success = 1,
        Failed = 2
    }

}
