using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Application.CategoryManagement.Commands.Create
{
    public class CreateCategoryDto
    {
        public string? Name { get; set; }

        [JsonIgnore]
        public string? Image { get; set; }

        public IFormFile? FromFileImages { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        //public string? CreatedBy { get; set; }

    }
}
