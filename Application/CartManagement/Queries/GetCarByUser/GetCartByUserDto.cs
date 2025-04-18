namespace Application.CartManagement.Queries.GetCarByUser
{
    public class GetCartByUserDto
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? UserName { get; set; }
        public Guid UserId { get; set; }
        public List<GetCartItemByUserDto> CartItems { get; set; } = new();
    }

    public class GetCartItemByUserDto
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
