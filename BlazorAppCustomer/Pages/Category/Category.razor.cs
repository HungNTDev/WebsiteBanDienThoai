using BlazorAppCustomer.Model.Base;
using BlazorAppCustomer.Model.Category;

namespace BlazorAppCustomer.Pages.Category
{
    public partial class Category
    {
        private PageResult<CategoryForView> Categories = new();

        private int PageIndex = 1;
        private int PageSize = 2;

        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
        }


        private async Task LoadCategories()
        {
            var newCategories = await CategoryService.GetCategoriesAsync(PageIndex, PageSize);
            Categories = newCategories;
        }

        private async Task PreviousPage()
        {
            if (PageIndex > 1)
            {
                PageIndex--;
                await LoadCategories();
            }
        }

        private async Task NextPage()
        {
            if (PageIndex >= Categories.TotalPages)
            {
                Console.WriteLine("⚠️ Đã ở trang cuối cùng, không gọi NextPage.");
                return;
            }
            PageIndex++;
            await LoadCategories();
        }

        private async Task GoToPage(int page)
        {
            PageIndex = page;
            await LoadCategories();
        }

    }
}
