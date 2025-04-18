namespace Application.CartManagement.Commands.UpdateCart
{
    public class UpdateCartDto
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public IList<CartItemForUpdate> CartItems { get; set; } = new List<CartItemForUpdate>();
    }
    public class CartItemForUpdate
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid? ProductId { get; set; }
    }
}
