using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Authentication;
using StockFlow.Application.Abstractions.Behaviors;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Users;

namespace StockFlow.Application.Users.Register;

internal sealed class RegisterUserCommandHandler : CommandHandlerBase<RegisterUserCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(
        IEnumerable<IValidator<RegisterUserCommand>> validators,
        IApplicationDbContext context,
        IPasswordHasher passwordHasher)
        : base(validators)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    }

    protected override async Task<Result<Guid>> HandleCore(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(u => u.Email == command.Email, cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            PasswordHash = _passwordHasher.Hash(command.Password),
            Role = command.Role
        };

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return user.Id;

    }
}
