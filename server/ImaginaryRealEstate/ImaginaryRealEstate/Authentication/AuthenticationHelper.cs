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
}