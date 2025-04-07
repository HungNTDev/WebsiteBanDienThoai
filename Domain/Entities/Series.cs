using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Series : BaseEntity
    {
        public string Name { get; set; }
        [ForeignKey("Brand")]
        public Guid? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
