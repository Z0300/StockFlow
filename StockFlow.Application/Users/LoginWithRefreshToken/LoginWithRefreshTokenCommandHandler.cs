using StockFlow.Application.Abstractions.Authentication;
using StockFlow.Application.Abstractions.Clock;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Users;

namespace StockFlow.Application.Users.LoginWithRefreshToken;

internal sealed class LoginWithRefreshTokenCommandHandler
    : ICommandHandler<LoginWithRefreshTokenCommand, TokenResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenProvider _tokenProvider;
    private readonly IDateTimeProvider _dateTimeProvider;

    public LoginWithRefreshTokenCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ITokenProvider tokenProvider,
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _tokenProvider = tokenProvider;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<TokenResponse>> Handle(LoginWithRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await _userRepository.GetRefreshTokenAsync(request.RefreshToken, cancellationToken);

        if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
        {
            throw new ApplicationException("The refresh token has expired");
        }

        string accessToken = _tokenProvider.Create(refreshToken.User);

        refreshToken.NewRefreshToken(_tokenProvider.GenerateRefreshToken(), _dateTimeProvider.UtcNow);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new TokenResponse(accessToken, refreshToken.Token);

    }
}
