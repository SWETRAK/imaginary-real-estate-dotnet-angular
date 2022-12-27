using System.Security.Claims;

namespace ImaginaryRealEstate.Authentication;

public static class AuthenticationHelper
{
    public static string GetUserId(ClaimsPrincipal user)
    {
        return user.Claims
            .ToDictionary(claim => claim.Type, claim => claim.Value)
            .FirstOrDefault(p => p.Key == ClaimTypes.NameIdentifier)
            .Value;
    }

    public static string GetAccessToken(HttpRequest httpRequest)
    {
        var splitTokenArray = httpRequest.Headers.Authorization.ToString().Split(" ");
        return splitTokenArray[1];
    }
}