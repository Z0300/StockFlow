using Microsoft.AspNetCore.Http;
using StockFlow.Application.Abstractions.Authentication;

namespace StockFlow.Infrastructure.Authentication;

internal sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    private sealed class UserContextUnavailableException() : Exception("User context is unavailable");

    public Guid UserId =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new UserContextUnavailableException();
}
