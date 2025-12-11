using FluentValidation;
using FluentValidation.Results;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockFlow.Application.Abstractions.Behaviors;

public static class ValidationHelper
{
    public static async Task<ValidationFailure[]> ValidateAsync<TCommand>(
        TCommand command,
        IEnumerable<IValidator<TCommand>> validators)
    {
        IValidator<TCommand>[] enumerable = validators?.ToArray() ?? [];
        if (enumerable.Length == 0)
        {
            return [];
        }

        var context = new ValidationContext<TCommand>(command);

        ValidationResult[] validationResults = await Task.WhenAll(
            enumerable.Select(v => v.ValidateAsync(context))
        ).ConfigureAwait(false);

        ValidationFailure[] validationFailures = [.. validationResults
            .Where(r => !r.IsValid)
            .SelectMany(r => r.Errors.ToArray())];

        return validationFailures;
    }

    public static ValidationError CreateValidationError(ValidationFailure[] validationFailures)
    {
        Error[] errors = [.. validationFailures.Select(f => Error.Problem(f.ErrorCode, f.ErrorMessage))];

        return new ValidationError(errors);
    }
}
