using System.Data;
using AgeCalculator;
using FluentValidation;
using ImaginaryRealEstate.Consts;
using ImaginaryRealEstate.Models.Auth;

namespace ImaginaryRealEstate.Validators.Auth;

public class RegisterUserWithPasswordDtoValidator: AbstractValidator<RegisterUserWithPasswordDto>
{
    public RegisterUserWithPasswordDtoValidator(DomainDbContext dbContext)
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty()
            .Custom((value, context) =>
            {
                var user = dbContext.Users.Any(x => x.Email == value.ToLower());
                if (user) context.AddFailure("Email", "That email is taken");
            });

        RuleFor(x => x.Password)
            .NotEmpty();

        RuleFor(x => x.RepeatPassword)
            .Equal(e => e.Password);

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