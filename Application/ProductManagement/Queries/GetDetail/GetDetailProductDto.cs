namespace Application.ProductManagement.Queries.GetDetail
{
    public class GetDetailProductDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SeriesId { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public ICollection<GetVariationDto> Variations { get; set; } = new List<GetVariationDto>();
    }
    public class GetVariationDto
    {
        public string? Name { get; set; }
        public ICollection<GetVariationOptionDto> Options { get; set; } =
            new List<GetVariationOptionDto>();
    }
    public class GetVariationOptionDto
    {
        public Guid OptionId { get; set; }
        public string? Value { get; set; }
    }
}
