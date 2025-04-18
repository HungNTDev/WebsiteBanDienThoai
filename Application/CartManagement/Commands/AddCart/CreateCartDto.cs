namespace Application.CartManagement.Commands.AddCart
{
    public class CreateCartDto
    {
        public Guid UserId { get; set; }
        public IList<CartItemForCreate> CartItems { get; set; } = new List<CartItemForCreate>();
    }
    public class CartItemForCreate
    {
        public int? Quantity { get; set; }
        public Guid? ProductId { get; set; }
    }
}
