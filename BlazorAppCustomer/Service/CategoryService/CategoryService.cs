using BlazorAppCustomer.Model.Base;
using BlazorAppCustomer.Model.Category;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BlazorAppCustomer.Service.CategoryService
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PageResult<CategoryForView>> GetCategoriesAsync(int pageIndex, int pageSize)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<PageResult<CategoryForView>>>(
                    $"Category?PageIndex={pageIndex}&PageSize={pageSize}");

                return response?.Data ??
                    new PageResult<CategoryForView>() { Items = new List<CategoryForView>(), TotalPages = 1 };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi API: {ex.Message}");
                return new PageResult<CategoryForView>();
            }
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

        public async Task<bool> UpdateCategoryAsync(UpdateCategory category, IBrowserFile? selectedFile)
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
                else
                {
                    // Nếu không có ảnh mới, giữ nguyên ảnh cũ bằng cách gửi nó lên API
                    content.Add(new StringContent(category.Image ?? ""), "ExistingImage");
                }

                var response = await _httpClient.PutAsync($"Category/{category.Id}", content);
                return response.IsSuccessStatusCode;
            }
        }

    }
}
