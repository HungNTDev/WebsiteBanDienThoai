using BlazorAppAdmin.Model.Base;
using BlazorAppAdmin.Model.Category;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace BlazorAppAdmin.Service.CategoryService
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryForView>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<PageResult<CategoryForView>>>(
                "https://localhost:7212/api/Category");

            if (response != null && response.IsSuccess && response.Data != null)
            {
                return response.Data.Items;
            }
            return new List<CategoryForView>();
        }

        public async Task<CategoryForView> GetCategoryByIdAsync(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<CategoryForView>($"Category/{id}");
            return response ?? new CategoryForView();
        }

        public async Task<bool> CreateCategoryAsync(CreateCategory category, IBrowserFile selectedFile)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(category.Name ?? ""), "Name");

                if (selectedFile != null)
                {
                    var stream = selectedFile.OpenReadStream();
                    var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    var fileContent = new StreamContent(memoryStream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(selectedFile.ContentType);

                    content.Add(fileContent, "FromFileImages", selectedFile.Name);
                }
                var response = await _httpClient.PostAsync("Category", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> UpdateCategoryAsync(UpdateCategory category, IBrowserFile selectedFile)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(category.Id.ToString()), "Id");
                content.Add(new StringContent(category.Name ?? ""), "Name");
                if (selectedFile != null)
                {
                    var stream = selectedFile.OpenReadStream();
                    var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;
                    var fileContent = new StreamContent(memoryStream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(selectedFile.ContentType);
                    content.Add(fileContent, "FromFileImages", selectedFile.Name);
                }
                var response = await _httpClient.PutAsync("Category", content);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
