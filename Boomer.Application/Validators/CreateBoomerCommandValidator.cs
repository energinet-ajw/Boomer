using Boomer.Application.Commands;
using FluentValidation;

namespace Boomer.Domain.Validators
{
    public sealed class CreateBoomerCommandValidator : AbstractValidator<CreateBoomerCommand>
    {
        public CreateBoomerCommandValidator()
        {
            RuleFor(x => x.Id).LessThan(10);
        }
    }
}
