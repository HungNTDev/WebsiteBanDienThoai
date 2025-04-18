﻿namespace Application.OrderManagement.Commands.Create
{
    public class CreateOrderDto
    {
        public Guid UserId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public decimal OrderTotal { get; set; }
    }

    public class OrderItemDto
    {
        public Guid ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
