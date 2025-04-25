using FluentValidation;
using Task_Management_Core.Features.Authentications.Commands.Models;

namespace Task_Management_Core.Features.Authentications.Commands.Validations
{
    public class LoginWithMicrosoftValidator : AbstractValidator<LoginWithMicrosoftCommand>
    {
        public LoginWithMicrosoftValidator()
        {
            ApplyValidationsRules();
        }

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("this field must not be empty")
                .NotNull().WithMessage("this field must not be null");


        }
    }
}
