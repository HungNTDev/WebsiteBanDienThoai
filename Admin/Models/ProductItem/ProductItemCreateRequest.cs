namespace Admin.Models.ProductItem
{
    public class ProductItemCreateRequest
    {
        public string Sku { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }
        public List<OptionRequest> Options { get; set; } = new();
    }
    public class OptionRequest
    {
        public string OptionId { get; set; }
    }
}
