namespace Application.VariationManagement.Queries.GetAll
{
    public class GetAllVariationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}
