using Microsoft.AspNetCore.Http;

namespace BlazorAppCustomer.Model.Category
{
    public class UpdateCategory
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public IFormFile? FromFileImages { get; set; }
    }
}
