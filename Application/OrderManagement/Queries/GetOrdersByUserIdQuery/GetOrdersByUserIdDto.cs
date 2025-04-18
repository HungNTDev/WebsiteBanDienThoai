namespace Application.OrderManagement.Queries.GetOrdersByUserIdQuery
{
    public class GetOrdersByUserIdDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public string Status { get; set; }
    }
}
