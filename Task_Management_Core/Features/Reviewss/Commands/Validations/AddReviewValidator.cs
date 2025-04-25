using FluentValidation;
using Task_Management_Core.Features.Reviewss.Commands.Models;

namespace Task_Management_Core.Features.Reviewss.Commands.Validations
{
    public class AddReviewValidator : AbstractValidator<AddReviewCommand>
    {
        public AddReviewValidator()
        {
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.UserId)
                 .NotEmpty().WithMessage("this field must not be empty")
                 .NotNull().WithMessage("this field must not be null");

            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("this field must not be empty")
                .NotNull().WithMessage("this field must not be null");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("this field must not be empty")
                .NotNull().WithMessage("this field must not be null");

        }


    }
}
