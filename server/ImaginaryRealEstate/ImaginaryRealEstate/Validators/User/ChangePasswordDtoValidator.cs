using FluentValidation;
using ImaginaryRealEstate.Models.Users;

namespace ImaginaryRealEstate.Validators.User;

public class ChangePasswordDtoValidator: AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .MinimumLength(8);
        
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(8);

        RuleFor(x => x.RepeatPassword)
            .NotEmpty()
            .Equal(e => e.NewPassword)
            .MinimumLength(8);
    }
}