using FluentValidation;
using PetStore.Application.Mouse;

namespace Boomer.Application.Validators
{
    public sealed class CreateBoomerCommandValidator : AbstractValidator<CreateMouseCommand>
    {
        public CreateBoomerCommandValidator()
        {
            //RuleFor(x => x.Id).LessThan(10);
        }
    }
}
