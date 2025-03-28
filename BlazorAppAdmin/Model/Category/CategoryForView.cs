﻿namespace BlazorAppAdmin.Model.Category
{
    public class CategoryForView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public IFormFile? FromFileImages { get; set; }
    }
}
