using FluentValidation;
using Task_Management_Core.Features.Authentications.Commands.Models;

namespace Task_Management_Core.Features.Authentications.Commands.Validations
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserValidator()
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.FullName)
                 .NotEmpty().WithMessage("this field must not be empty")
                 .NotNull().WithMessage("this field must not be null")
                 .MaximumLength(100).WithMessage("max length must not exceeded 100");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("this field must not be empty")
                .NotNull().WithMessage("this field must not be null")
                .MaximumLength(100).WithMessage("max length must not exceeded 100");

            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("this field must not be empty")
                 .NotNull().WithMessage("this field must not be null");

            RuleFor(x => x.Password)
                 .NotEmpty().WithMessage("this field must not be empty")
                 .NotNull().WithMessage("this field must not be null");



        }

        public void ApplyCustomValidationsRules()
        {

        }

    }
}
