using Microsoft.AspNetCore.Components.Forms;

namespace Admin.Models.Product
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }


        public string Description { get; set; }


        public decimal Price { get; set; }


        public string CategoryId { get; set; }


        public string SeriesId { get; set; }

        public IBrowserFile Image { get; set; }
    }
}
