using FluentValidation;
using Task_Management_Core.Features.Filess.Commands.Models;

namespace Task_Management_Core.Features.Filess.Commands.Validations
{
    public class AddFileToGoogleDriveValidator : AbstractValidator<AddFileToGoogleDriveCommand>
    {
        public AddFileToGoogleDriveValidator()
        {
            ApplyValidationsRules();
        }

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.FileURL)
                .NotEmpty().WithMessage("this field must not be empty")
                .NotNull().WithMessage("this field must not be null");
            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("this field must not be empty")
                .NotNull().WithMessage("this field must not be null");
            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("this field must not be empty")
                .NotNull().WithMessage("this field must not be null");

        }
    }
}
