using FluentValidation;
using Task_Management_Core.Features.Authentications.Commands.Models;

namespace Task_Management_Core.Features.Authentications.Commands.Validations
{
    public class LoginWithGoogleValidator : AbstractValidator<LoginWithGoogleCommand>
    {
        public LoginWithGoogleValidator()
        {
            ApplyValidationsRules();
        }

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.tokenId)
                .NotEmpty().WithMessage("this field must not be empty")
                .NotNull().WithMessage("this field must not be null");
            RuleFor(x => x.GoogleAccessToken)
                .NotEmpty().WithMessage("this field must not be empty")
                .NotNull().WithMessage("this field must not be null");

        }
    }
}
