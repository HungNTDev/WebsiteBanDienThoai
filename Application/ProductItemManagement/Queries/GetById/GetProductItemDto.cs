namespace Application.ProductItemManagement.Queries.GetById
{
    public class GetProductItemByIdDto
    {
        public Guid Id { get; set; }
        public string SKU { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public List<Guid> VariationOptions { get; set; } = new();
    }
}
