namespace Application.BrandManagement.Queries.GetAll
{
    public class GetAllBrandDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}
