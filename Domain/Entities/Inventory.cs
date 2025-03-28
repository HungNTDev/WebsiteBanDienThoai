using Domain.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Inventory : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public int StockQuantity { get; set; }
        public int ReservedQuantity { get; set; }

        
        public Guid ProductItemId { get; set; }
        public ProductItem? ProductItem { get; set; }
    }
}
