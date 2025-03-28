using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VariationManagement.Commands.Create
{
    public class CreateVariationValidation : AbstractValidator<CreateVariationDto>
    {
        public CreateVariationValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Khong duoc rong")
                .NotNull().WithMessage("Khong duoc null");
        }
    }
}
