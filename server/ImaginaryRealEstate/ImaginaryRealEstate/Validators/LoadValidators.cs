using FluentValidation;
using FluentValidation.AspNetCore;
using ImaginaryRealEstate.Models.Auth;
using ImaginaryRealEstate.Validators.Auth;

namespace ImaginaryRealEstate.Validators;

public static class LoadValidators
{
    public static IServiceCollection AddValiadators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddScoped<IValidator<RegisterUserWithPasswordDto>, RegisterUserWithPasswordDtoValidator>();
        services.AddScoped<IValidator<LoginUserWithPasswordDto>, LoginUserWithPasswordDtoValidator>();
        
        
        return services;
    }
}