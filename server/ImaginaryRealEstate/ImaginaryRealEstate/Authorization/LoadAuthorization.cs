using ImaginaryRealEstate.Entities;
using Microsoft.AspNetCore.Identity;

namespace ImaginaryRealEstate.Authorization;

public static class LoadAuthorization
{
    public static IServiceCollection AddAuthorizationCustom(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            // Example of policy
            // options.AddPolicy("IsAuthor", policyBuilder =>
            // {
            //     policyBuilder.RequireRole("Author");
            // });
        });
        
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }
}