using StockFlow.Application.Abstractions.Authentication;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Users;
using StockFlow.Domain.Entities.Users.ValueObjects;

namespace StockFlow.Application.Users.Login;

internal sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, AccessTokenResponse>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUserRepository _userRepository;

    public LoginUserCommandHandler(
        IPasswordHasher passwordHasher,
        ITokenProvider tokenProvider,
        IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
        _userRepository = userRepository;
    }
    public async Task<Result<AccessTokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailAsync(new Email(request.Email), cancellationToken);

        if (user is null)
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        }

        bool verified = _passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        }

        string token = _tokenProvider.Create(user);

        return new AccessTokenResponse(token);
    }
}

