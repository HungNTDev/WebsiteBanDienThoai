using System.ComponentModel.DataAnnotations;

namespace Admin.Models.ProductItem
{
    public class ProductItem
    {
        public string Id { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string ProductName { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<string> VariationOptions { get; set; }
    }


}