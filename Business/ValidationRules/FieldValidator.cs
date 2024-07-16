using Entities.Dto;
using FluentValidation;

namespace Business.ValidationRules;
public class FieldValidator : AbstractValidator<TestValidationDto>
{
    public FieldValidator()
    {
        RuleFor(x => x.Data).NotEmpty().WithMessage("Bu alan zorunludur");
    }
}
