using Microsoft.AspNetCore.Http;
using StockFlow.Application.Abstractions.Authentication;

namespace StockFlow.Infrastructure.Authentication;

internal sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
#pragma warning disable S3871 // Exception types should be "public"
    public sealed class UserContextUnavailableException() : Exception("User context is unavailable");
#pragma warning restore S3871 // Exception types should be "public"

    public Guid UserId =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new UserContextUnavailableException();
}
