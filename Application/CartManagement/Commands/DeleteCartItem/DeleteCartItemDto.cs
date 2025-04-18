namespace Application.CartManagement.Commands.DeleteCartItem
{
    public class DeleteCartItemDto
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
