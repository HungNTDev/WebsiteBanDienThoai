using Domain.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class CategoryBrand : IEntity
    {
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }
        public Brand? Brand { get; set; }
    }
}
