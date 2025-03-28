namespace BlazorAppAdmin.Model.Category
{
    public class CreateCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IFormFile? FromFileImages { get; set; }
    }
}
