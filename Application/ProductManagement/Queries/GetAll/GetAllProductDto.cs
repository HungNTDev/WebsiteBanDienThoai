namespace Application.ProductManagement.Queries.GetAll
{
    public class GetAllProductDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public decimal? Price { get; set; }
    }
}
