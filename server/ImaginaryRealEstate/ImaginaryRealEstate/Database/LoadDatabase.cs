using ImaginaryRealEstate.Database.Interfaces;
using ImaginaryRealEstate.Settings;
using MongoDB.Driver;

namespace ImaginaryRealEstate.Database;

public static class LoadDatabase
{
    public static void AddMongoDatabase(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var environment = builder.Environment;
        
        var mongoSetting = new MongoSettings();
        configuration.GetSection("MongoDB").Bind(mongoSetting);
        
        services.AddSingleton<IMongoClient>(new MongoClient("mongodb://localhost:27017/project-octopus"));
        
        services.AddScoped<IOfferRepository, OfferRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        services.AddSingleton(mongoSetting);

        services.AddSingleton<IMongoClient>(new MongoClient(mongoSetting.ConnectionString));
        
    }
}