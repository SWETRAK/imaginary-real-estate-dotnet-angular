using AgeCalculator;
using FluentValidation;
using ImaginaryRealEstate.Consts;
using ImaginaryRealEstate.Database;
using ImaginaryRealEstate.Database.Interfaces;
using ImaginaryRealEstate.Models.Auth;

namespace ImaginaryRealEstate.Validators.Auth;

public class RegisterUserWithPasswordDtoValidator: AbstractValidator<RegisterUserWithPasswordDto>
{
    public RegisterUserWithPasswordDtoValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty()
            .Custom(async (value, context) =>
            {
                var user = await userRepository.GetByEmail(value);
                if (user is not null) context.AddFailure("Email", "That email is taken");
            });

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);

        RuleFor(x => x.RepeatPassword)
            .NotEmpty()
            .Equal(e => e.Password)
            .MinimumLength(8);

        RuleFor(x => x.FirstName)
            .NotEmpty();
        
        RuleFor(x => x.LastName)
            .NotEmpty();

        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .Custom((value,  context) =>
            {
                var age = new Age(value, DateTime.Now);
                if (age.Years < 18) context.AddFailure("DateOfBirth", "You are too young to register");
            });

        RuleFor(x => x.Role)
            .NotEmpty()
            .Custom((value, context) =>
            {
                var roles = Roles.GetAllRoles();
                if (!roles.Contains(value)) context.AddFailure("Role", "Unknown role");
            });
    }
}