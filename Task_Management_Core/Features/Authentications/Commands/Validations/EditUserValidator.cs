using FluentValidation;
using Task_Management_Core.Features.Authentications.Commands.Models;


namespace Task_Management_Core.Features.Authentications.Commands.Validations
{
    public class EditUserValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserValidator()
        {
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("this field must not be empty")
                .NotNull().WithMessage("this field must not be null");
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("this field must not be empty")
                .NotNull().WithMessage("this field must not be null");
        }
    }
}
