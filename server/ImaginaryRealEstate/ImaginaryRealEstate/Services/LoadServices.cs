using ImaginaryRealEstate.Models.Offers;
using ImaginaryRealEstate.Services.Interfaces;

namespace ImaginaryRealEstate.Services;

public static class LoadServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IS3Service, S3Service>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IOfferService, OfferService>();
        
        services.AddScoped<IImageService, ImageService>();
        
        return services;
    }
}