using FluentValidation;

namespace StockFlow.Application.Users.RevokeRefreshToken;

internal sealed class RevokeRefreshTokenCommandValidator : AbstractValidator<RevokeRefreshTokenCommand>
{
    public RevokeRefreshTokenCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotEqual(Guid.Empty);
    }
}
