using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.Product
{
    public class ProductUpdateRequest
    {
        public string Id { get; set; }


        public string? Name { get; set; }


        public string? Description { get; set; }


        public decimal? Price { get; set; }


        public string? CategoryId { get; set; }

        public string? SeriesId { get; set; }


    }
}
