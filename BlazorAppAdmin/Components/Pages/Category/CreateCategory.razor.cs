using Microsoft.AspNetCore.Components.Forms;

namespace BlazorAppAdmin.Components.Pages.Category
{
    public partial class CreateCategory
    {
        private string categoryName = "";
        private IBrowserFile? selectedFile;
        private string message = "";

        private void HandleFileSelected(InputFileChangeEventArgs e)
        {
            selectedFile = e.File;
        }

        private async Task InsertCategory()
        {
            if (string.IsNullOrWhiteSpace(categoryName) || selectedFile == null)
            {
                message = "Please enter a category name and select an image.";
                return;
            }

            // Create category object
            var newCategory = new BlazorAppAdmin.Model.Category.CreateCategory
            {
                Name = categoryName
            };

            var isSuccess = await CategoryService
            .CreateCategoryAsync(newCategory, selectedFile);

            if (isSuccess)
            {
                message = "Category created successfully!";
                Navigation.NavigateTo("/categories");
            }
            else
            {
                message = "An error occurred while creating the category.";
            }
        }

        private void Cancel()
        {
            Navigation.NavigateTo("/categories");
        }
    }
}
