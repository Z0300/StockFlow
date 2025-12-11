using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using SharedKernel;
using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Abstractions.Behaviors;

public abstract class CommandHandlerBase<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
{
    private readonly IEnumerable<IValidator<TCommand>> _validators;

    protected CommandHandlerBase(IEnumerable<IValidator<TCommand>> validators)
    {
        _validators = validators;
    }

    public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        // run validators (ValidationHelper from your code)
        ValidationFailure[] failures = await ValidationHelper.ValidateAsync(command, _validators).ConfigureAwait(false);
        if (failures.Length > 0)
        {
            return Result.Failure<TResponse>(ValidationHelper.CreateValidationError(failures));
        }

        return await HandleCore(command, cancellationToken).ConfigureAwait(false);
    }

    // concrete handlers implement business logic here
    protected abstract Task<Result<TResponse>> HandleCore(TCommand command, CancellationToken cancellationToken);
}

public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
{
    private readonly IEnumerable<IValidator<TCommand>> _validators;

    protected CommandHandlerBase(IEnumerable<IValidator<TCommand>> validators)
    {
        _validators = validators;
    }

    public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
    {
        ValidationFailure[] failures = await ValidationHelper.ValidateAsync(command, _validators).ConfigureAwait(false);
        if (failures.Length > 0)
        {
            return Result.Failure(ValidationHelper.CreateValidationError(failures));
        }

        return await HandleCore(command, cancellationToken).ConfigureAwait(false);
    }

    protected abstract Task<Result> HandleCore(TCommand command, CancellationToken cancellationToken);
}
