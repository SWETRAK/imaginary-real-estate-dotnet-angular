using ImaginaryRealEstate.Exceptions.Auth;
using ImaginaryRealEstate.Exceptions.Offer;

namespace ImaginaryRealEstate.Middlewares;

public class ErrorHandlingMiddleware: IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NoGuidException e)
        {
            _logger.LogWarning("Can not parse Guid from string received from request => {}", e.Message);
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(e.Message);
        }
        catch (OfferNotFountException e)
        {
            _logger.LogWarning("Offer with provided Guid not found => {}", e.Message);
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("Offer with those identifier not found");
        }
        catch (InvalidLoginDataException e)
        {
            _logger.LogWarning("User tried login with wrong credentials => {}", e.Message);
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Incorrect email or password");
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError("InvalidOperationException error {Error} => {Message}", e, e.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Internal server error");
        }
        catch (Exception e)
        {
            _logger.LogError("Unknown error {Error} => {Message}", e, e.Message);
            Console.Write(e.GetType());
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Internal server error");
        }
    }
}