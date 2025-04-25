using FluentValidation;
using Task_Management_Core.Features.Projectss.Commands.Models;

namespace Task_Management_Core.Features.Projectss.Commands.Validations
{
    public class AddProjectValidator : AbstractValidator<AddProjectCommand>
    {
        public AddProjectValidator()
        {
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("this field must not be empty")
                 .NotNull().WithMessage("this field must not be null");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("this field must not be empty")
                .NotNull().WithMessage("this field must not be null");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("this field must not be empty")
                .NotNull().WithMessage("this field must not be null");

            RuleFor(x => x.EndDate)
               .NotEmpty().WithMessage("this field must not be empty")
               .NotNull().WithMessage("this field must not be null");


        }
    }
}
