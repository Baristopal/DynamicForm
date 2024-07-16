using Entities.Dto;
using FluentValidation;

namespace Business.ValidationRules;
public class EditFormValidator : AbstractValidator<FormDto>
{
    public EditFormValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(s => s.Description).NotEmpty().WithMessage("Description is required");
        //RuleFor(s => s.Fields).NotEmpty().WithMessage("Fields are required");
    }
}
