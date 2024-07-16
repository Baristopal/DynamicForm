using Entities.Dto;
using FluentValidation;

namespace Business.ValidationRules;
public class UserLoginValidator : AbstractValidator<UserForLoginDto>
{
    public UserLoginValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username cannot be empty");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
    }
}
