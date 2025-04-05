using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Admin.Models.Category
{
    public class CategoryUpdateRequest
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name must be between {2} and {1} characters", MinimumLength = 2)]
        public string? Name { get; set; }

        public string? Image { get; set; }

        [JsonIgnore]
        public IFormFile? FromFileImages { get; set; }

        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}