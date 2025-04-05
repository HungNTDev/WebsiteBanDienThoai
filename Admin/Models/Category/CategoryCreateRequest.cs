using System.ComponentModel.DataAnnotations;

namespace Admin.Models.Category
{
    public class CategoryCreateRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name must be between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select an image")]
        public string Image { get; set; }
    }
} 