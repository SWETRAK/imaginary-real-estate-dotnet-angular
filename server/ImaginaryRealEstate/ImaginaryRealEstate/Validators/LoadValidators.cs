using FluentValidation;
using FluentValidation.AspNetCore;
using ImaginaryRealEstate.Models.Auth;
using ImaginaryRealEstate.Models.Offers;
using ImaginaryRealEstate.Validators.Auth;
using ImaginaryRealEstate.Validators.Offer;

namespace ImaginaryRealEstate.Validators;

public static class LoadValidators
{
    public static IServiceCollection AddValiadators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddScoped<IValidator<RegisterUserWithPasswordDto>, RegisterUserWithPasswordDtoValidator>();
        services.AddScoped<IValidator<LoginUserWithPasswordDto>, LoginUserWithPasswordDtoValidator>();

        services.AddScoped<IValidator<NewOfferIncomingDto>, NewOfferIncomingDtoValidator>();
        
        
        return services;
    }
}