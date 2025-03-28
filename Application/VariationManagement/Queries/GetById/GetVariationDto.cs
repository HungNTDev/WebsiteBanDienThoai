namespace Application.VariationManagement.Queries.GetById
{
    public class GetVariationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}