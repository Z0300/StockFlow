using FluentValidation;

namespace StockFlow.Application.Transactions.Create;

internal sealed class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("At least one item is required.");

        RuleFor(x => x.WarehouseId)
           .NotEmpty();

        RuleFor(x => x.TransactionType)
          .IsInEnum();

        RuleForEach(x => x.Items).ChildRules(items =>
        {
            items.RuleFor(i => i.ProductId)
                .NotEmpty();

            items.RuleFor(i => i.QuantityChange)
                .NotEqual(0);

            items.RuleFor(i => i.UnitCost)
                .GreaterThanOrEqualTo(0);

        });
    }
}
