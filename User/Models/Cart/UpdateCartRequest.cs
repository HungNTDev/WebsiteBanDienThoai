namespace User.Models.Cart
{
    public class UpdateCartRequest
    {
        public Guid Id { get; set; } // CartId
        public Guid UserId { get; set; }
        public List<UpdateCartItemDto> CartItems { get; set; } = new();
    }

    public class UpdateCartItemDto
    {
        public Guid Id { get; set; } // CartItemId
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
    }

}
