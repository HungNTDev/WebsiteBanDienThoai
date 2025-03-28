using Domain.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserAddress : IEntity
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [Key]
        [ForeignKey("Address")]
        public Guid AddressId { get; set; }
        public Address? Address { get; set; }
    }
}
