namespace Admin.Models.ProductItem
{
    public class ProductItemDetailResponse
    {
        public string Id { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string ProductName { get; set; }
        public string Name { get; set; }
        public List<OptionDetail> VariationOptions { get; set; }
    }
    public class OptionDetail
    {
        public string OptionId { get; set; }
    }
}
