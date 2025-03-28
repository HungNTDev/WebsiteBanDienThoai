using Domain.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Review : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string? Rating { get; set; }
        public string? Comment { get; set; }

        [ForeignKey("ApplicationUser")]
        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [ForeignKey("OrderItem")]
        public Guid OrderItemId { get; set; }
        public OrderItem? OrderItem { get; set; }
    }
}
