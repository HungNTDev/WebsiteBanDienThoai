namespace User.Models.Cart
{
    public class DeleteCartItemRequest
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
