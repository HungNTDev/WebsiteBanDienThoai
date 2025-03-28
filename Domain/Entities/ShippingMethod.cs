using Domain.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ShippingMethod : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
