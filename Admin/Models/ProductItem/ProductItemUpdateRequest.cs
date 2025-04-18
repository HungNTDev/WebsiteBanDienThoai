namespace Admin.Models.ProductItem
{
    public class ProductItemUpdateRequest
    {
        public string Id { get; set; }
        public string Sku { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }
        public List<UpdateOptionRequest> Options { get; set; } = new();
    }
    public class UpdateOptionRequest
    {
        public string OptionId { get; set; }
    }
    public class ProductItemDetailUpdateResponse
    {
        public string Id { get; set; }
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string ProductId { get; set; }
        public List<ProductItemOptionResponse> Options { get; set; }
    }

    public class ProductItemOptionResponse
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public string VariationId { get; set; }
        public string VariationName { get; set; }
    }
}
