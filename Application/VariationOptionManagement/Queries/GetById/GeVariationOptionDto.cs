namespace Application.VariationOptionManagement.Queries.GetById
{
    public class GeVariationOptionDto
    {
        public Guid Id { get; set; }
        public string? Value { get; set; }

        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
