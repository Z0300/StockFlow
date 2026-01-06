using StockFlow.Application.Abstractions.Authentication;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Users;

namespace StockFlow.Application.Users.RevokeRefreshToken;

internal sealed class RevokeRefreshTokenCommandHandler
    : ICommandHandler<RevokeRefreshTokenCommand, bool>
{
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepository;

    public RevokeRefreshTokenCommandHandler(
        IUserContext userContext,
        IUserRepository userRepository)
    {
        _userContext = userContext;
        _userRepository = userRepository;
    }
    public async Task<Result<bool>> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId != _userContext.UserId)
        {
            throw new ApplicationException("You can't do this");
        }

        await _userRepository.RevokeRefreshTokenAsync(request.UserId, cancellationToken);

        return true;
    }
}
