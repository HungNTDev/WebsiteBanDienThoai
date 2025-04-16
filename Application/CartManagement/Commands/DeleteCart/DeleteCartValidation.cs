using FluentValidation;

namespace Application.CartManagement.Commands.DeleteCart
{
    public class DeleteCartValidation : AbstractValidator<DeleteCartDto>
    {
        public DeleteCartValidation()
        {

        }
    }
}
