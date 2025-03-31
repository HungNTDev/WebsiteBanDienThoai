using Domain.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Address : BaseEntity
    {
        public string? Unit_number { get; set; }
        public string? Address_line1 { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<UserAddress>? UserAddresses { get; set; }
    }
}
