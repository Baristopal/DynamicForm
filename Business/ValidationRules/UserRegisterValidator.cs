using Entities.Dto;
using FluentValidation;

namespace Business.ValidationRules;
public class UserRegisterValidator : AbstractValidator<UserForRegisterDto>
{
    public UserRegisterValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
    }

}
