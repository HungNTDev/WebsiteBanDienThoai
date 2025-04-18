namespace Admin.Models.ProductItem
{
    public class VariationWithOptions
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<VariationOption> Options { get; set; } = new();
    }
}
