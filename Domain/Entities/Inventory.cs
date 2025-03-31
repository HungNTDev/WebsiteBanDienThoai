namespace Domain.Entities
{
    public class Inventory : BaseEntity
    {
        public int StockQuantity { get; set; }
        public int ReservedQuantity { get; set; }


        public Guid ProductItemId { get; set; }
        public ProductItem? ProductItem { get; set; }
    }
}
