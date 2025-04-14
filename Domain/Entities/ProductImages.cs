namespace Domain.Entities
{
    public class ProductImages : BaseEntity
    {
        public string Url { get; set; }
        public Guid ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }
    }
}
