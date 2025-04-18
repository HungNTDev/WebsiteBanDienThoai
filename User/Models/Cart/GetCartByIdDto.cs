namespace User.Models.Cart
{
    public class GetCartByIdDto
    {
        public Guid Id { get; set; }

        public string? UserName { get; set; }

        public Guid UserId { get; set; }
        public List<GetCartItem> CartItems { get; set; } = new();
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
