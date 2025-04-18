namespace Application.VariationOptionManagement.Queries.GetAll
{
    public class GetAllVariationOptionDto
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public Guid VariationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}
