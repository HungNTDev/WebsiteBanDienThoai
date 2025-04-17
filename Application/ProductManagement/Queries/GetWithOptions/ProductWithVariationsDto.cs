namespace Application.ProductManagement.Queries.GetWithOptions
{
    public class ProductWithVariationsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal? Price { get; set; }
        public List<VariationDto> Variations { get; set; } = new();
    }

    public class VariationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<VariationOptionDto> Options { get; set; } = new();
    }

    public class VariationOptionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
