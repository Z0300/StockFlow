using StockFlow.Application.Abstractions.Authentication;
using StockFlow.Application.Abstractions.Clock;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Users;
using StockFlow.Domain.Entities.Users.ValueObjects;

namespace StockFlow.Application.Users.Login;

internal sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, TokenResponse>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUserRepository _userRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public LoginUserCommandHandler(
        IPasswordHasher passwordHasher,
        ITokenProvider tokenProvider,
        IUserRepository userRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
    {
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
        _userRepository = userRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<TokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailAsync(new Email(request.Email), cancellationToken);

        if (user is null)
        {
            return Result.Failure<TokenResponse>(UserErrors.InvalidCredentials);
        }

        bool verified = _passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<TokenResponse>(UserErrors.InvalidCredentials);
        }

        string token = _tokenProvider.Create(user);

        var refreshToken = RefreshToken.Create(
            _tokenProvider.GenerateRefreshToken(),
            user.Id,
            _dateTimeProvider.UtcNow.AddDays(7));

        _userRepository.AddRefresToken(refreshToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new TokenResponse(token, refreshToken.Token);
    }
}

