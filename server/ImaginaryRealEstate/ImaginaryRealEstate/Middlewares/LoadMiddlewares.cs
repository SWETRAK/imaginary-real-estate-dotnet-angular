namespace ImaginaryRealEstate.Middlewares;

public static class LoadMiddlewares
{
    public static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<ErrorHandlingMiddleware>();

        return services;
    }
    
    public static void UseMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}