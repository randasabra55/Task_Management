using FluentValidation;
using Task_Management_Core.Features.Authentications.Commands.Models;



namespace Task_Management_Core.Features.Authentications.Commands.Validations
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            ApplyValidationsRules();
        }

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("this field must not be empty")
                .NotNull().WithMessage("this field must not be null");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("this field must not be empty")
                .NotNull().WithMessage("this field must not be null");

        }
    }
}
