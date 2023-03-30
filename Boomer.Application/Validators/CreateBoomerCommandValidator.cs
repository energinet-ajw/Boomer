using Boomer.Application.Mouse;
using Boomer.Application.Commands;
using FluentValidation;

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
