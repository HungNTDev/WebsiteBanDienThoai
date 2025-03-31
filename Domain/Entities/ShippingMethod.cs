namespace Domain.Entities
{
    public class ShippingMethod : BaseEntity
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
