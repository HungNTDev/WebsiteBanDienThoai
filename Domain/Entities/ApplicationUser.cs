using Domain.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity
    {
        public string? FullName { get; set; }
        public string? Password { get; set; }
        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? FromFileImages { get; set; }

        public ICollection<Cart>? Carts { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<UserAddress>? UserAddresses { get; set; }
        public ICollection<UserPayment>? Payments { get; set; }
    }
}
