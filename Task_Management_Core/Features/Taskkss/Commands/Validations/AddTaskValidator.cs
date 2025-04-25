using FluentValidation;
using Task_Management_Core.Features.Taskkss.Commands.Models;

namespace Task_Management_Core.Features.Taskkss.Commands.Validations
{
    public class AddTaskValidator : AbstractValidator<AddTaskCommand>
    {
        public AddTaskValidator()
        {
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Title)
                 .NotEmpty().WithMessage("this field must not be empty")
                 .NotNull().WithMessage("this field must not be null");

            RuleFor(x => x.Status)
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

            RuleFor(x => x.ProjectId)
               .NotEmpty().WithMessage("this field must not be empty")
               .NotNull().WithMessage("this field must not be null");

            RuleFor(x => x.UserId)
               .NotEmpty().WithMessage("this field must not be empty")
               .NotNull().WithMessage("this field must not be null");
        }
    }
}

