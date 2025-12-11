using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Authentication;
using StockFlow.Application.Abstractions.Behaviors;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Domain.Users;

namespace StockFlow.Application.Users.Login;

internal sealed class LoginUserCommandHandler : CommandHandlerBase<LoginUserCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;

    public LoginUserCommandHandler(
        IEnumerable<IValidator<LoginUserCommand>> validators,
        IApplicationDbContext context,
         IPasswordHasher passwordHasher,
         ITokenProvider tokenProvider
        ) : base(validators)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
    }

    protected override async Task<Result<string>> HandleCore(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        bool verified = _passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        string token = _tokenProvider.Create(user);

        return token;

    }

}

