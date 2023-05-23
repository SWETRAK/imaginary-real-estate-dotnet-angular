using ImaginaryRealEstate.Services.Interfaces;
using ImaginaryRealEstate.Settings;

namespace ImaginaryRealEstate.Services;

public static class LoadServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        services.AddScoped<IMinioService, MinioService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IOfferService, OfferService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IUserService, UserService>();
        
        return services;
    }
}