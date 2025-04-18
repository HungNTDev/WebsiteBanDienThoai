using FluentValidation;

namespace Application.CartManagement.Commands.UpdateCart
{
    public class UpdateCartValidation : AbstractValidator<UpdateCartDto>
    {
        public UpdateCartValidation() { }
    }
}
