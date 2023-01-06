using FluentValidation;
using ImaginaryRealEstate.Models.Auth;

namespace ImaginaryRealEstate.Validators.Auth;

public class LoginUserWithPasswordDtoValidator: AbstractValidator<LoginUserWithPasswordDto>
{
    public LoginUserWithPasswordDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}