namespace Application.ProductItemManagement.Queries.GetByOptions
{
    public class GetProductItemDto
    {
        public Guid Id { get; set; }
        public string SKU { get; set; } = default!;
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public ICollection<VariationDto> Variations { get; set; } = new List<VariationDto>();
    }
    public class VariationDto
    {
        public string? Name { get; set; }
        public ICollection<VariationOptionDto> Options { get; set; } = new List<VariationOptionDto>();
    }
    public class VariationOptionDto
    {
        public string? Value { get; set; }
    }
}
