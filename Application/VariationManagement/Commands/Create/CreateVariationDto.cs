namespace Application.VariationManagement.Commands.Create
{
    public class CreateVariationDto
    {
        public string? Name { get; set; }
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
