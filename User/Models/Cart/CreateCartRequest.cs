namespace User.Models.Cart
{
    public class CreateCartRequest
    {
        public Guid UserId { get; set; }
        public List<CartItemDto> CartItems { get; set; } = new();
    }

    public class CartItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

}
