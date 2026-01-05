using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace StockFlow.Infrastructure.Authentication;

internal static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        string userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.TryParse(userId, out Guid parsedUserId)
            ? parsedUserId :
            throw new ApplicationException("User identifier is unavailable");
    }
}
