namespace Application.OrderManagement.Queries.GetOrderHistory
{
    public class OrderHistoryDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<OrderItemHistoryDto> Items { get; set; } = new();
    }

    public class OrderItemHistoryDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
