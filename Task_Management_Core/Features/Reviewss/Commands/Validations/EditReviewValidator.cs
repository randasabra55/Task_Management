using FluentValidation;
using Task_Management_Core.Features.Reviewss.Commands.Models;

namespace E_Learning_Core.Features.Reviewss.Commands.Validations
{
    public class EditReviewValidator : AbstractValidator<EditReviewCommand>
    {
        public EditReviewValidator()
        {
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage("this field must not be empty")
                 .NotNull().WithMessage("this field must not be null");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("this field must not be empty")
                .NotNull().WithMessage("this field must not be null");
        }


    }
}
