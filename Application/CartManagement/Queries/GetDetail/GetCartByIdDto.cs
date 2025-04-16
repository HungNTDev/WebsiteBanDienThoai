﻿namespace Application.CartManagement.Queries.GetCartById
{
    public class GetCartByIdDto
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? UserName { get; set; }
        public string? Address { get; set; }
        public string? UserId { get; set; }
        public double Amount { get; set; }
        public List<GetCartItem> CartItems { get; set; } = new List<GetCartItem>();
    }
    public class GetCartItem
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public Guid ProductItemId { get; set; }
        public string? ProductItemImage { get; set; }
        public string? ProductItemName { get; set; }
    }
}

