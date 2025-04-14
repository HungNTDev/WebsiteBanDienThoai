namespace Application.ProductItemManagement.Queries.GetAll
{
    public class GetAllProductItemDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
