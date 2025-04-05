namespace Application.VariationManagement.Queries.GetById
{
    public class GetVariationOptionDto
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}