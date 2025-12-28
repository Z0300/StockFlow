using Microsoft.AspNetCore.Http;
using StockFlow.Application.Abstractions.Authentication;

namespace StockFlow.Infrastructure.Authentication;

internal sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserContext(IHttpContextAccessor contextAccessor) => _contextAccessor = contextAccessor;

    public Guid UserId =>
        _contextAccessor
            .HttpContext?.User
            .GetUserId() ?? throw new ApplicationException("User context is unavailable");
}
