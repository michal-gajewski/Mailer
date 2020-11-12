using DataAccess.Commands;
using FluentValidation;

namespace Domain.Validators
{
    public class AddEmailCommandValidator : AbstractValidator<AddEmailCommand>
    {
        public AddEmailCommandValidator()
        {
            RuleFor(email => email.Text).NotEmpty().NotNull();
            RuleFor(email => email.Title).NotEmpty().NotNull();
        }
    }
}