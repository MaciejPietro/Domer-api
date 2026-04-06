using FluentValidation;
using Kompass.Application.Common.Validation;

namespace Kompass.Application.Commands.Auth.ConfirmEmail;

public class ConfirmEmailCommandValidator: AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        // EMAIL
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MustBeValidEmail();
    }
}