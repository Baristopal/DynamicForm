using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules;
public class EditFormValidator : AbstractValidator<Form>
{
    public EditFormValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(s=>s.Description).NotEmpty().WithMessage("Description is required");
    }
}
