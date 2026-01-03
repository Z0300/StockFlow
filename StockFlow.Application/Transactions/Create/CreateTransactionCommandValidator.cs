using FluentValidation;
using StockFlow.Domain.Entities.Transactions.Enums;

namespace StockFlow.Application.Transactions.Create;

internal sealed class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.WarehouseId).NotEmpty();
        RuleFor(x => x.TransactionType).IsInEnum();

        RuleFor(x => x.Items)
             .NotEmpty()
             .WithMessage("At least one item is required.");

        // Opening Balance Validation
        When(x => x.TransactionType == TransactionType.OpeningBalance, () =>
        {
            RuleFor(x => x.OrderId).Null().Empty();

            RuleFor(x => x.Items)
            .Must(items =>
                    items.GroupBy(i => i.ProductId)
                         .All(g => g.Count() == 1))
                .WithMessage("Duplicate products in opening balance are not allowed");

            RuleForEach(x => x.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.QuantityChange).GreaterThan(0);
                items.RuleFor(i => i.UnitCost).GreaterThan(0);
            });
        });

        // Purchase Receipt Validation
        When(x => x.TransactionType == TransactionType.PurchaseReceipt, () =>
        {
            RuleFor(x => x.OrderId).NotEmpty()
                .WithMessage($"Order ID is required for transaction ");

            RuleFor(x => x.Items)
                .Must(items => items.All(g => g.QuantityChange > 0))
                    .WithMessage("Receipt must increase stock");

        });

        // Sale Issue Validation
        When(x => x.TransactionType == TransactionType.SalesIssue, () =>
        {
            RuleFor(x => x.OrderId).NotNull().NotEqual(Guid.Empty);

            RuleFor(x => x.Items)
               .Must(items => items.All(g => g.QuantityChange >= 0))
                   .WithMessage("Sale must reduce stock");

        });

        // Return to supplier Validation
        When(x => x.TransactionType == TransactionType.ReturnToSupplier, () =>
        {
            RuleFor(x => x.OrderId).NotNull().NotEqual(Guid.Empty);

            RuleFor(x => x.Items)
              .Must(items => items.All(g => g.QuantityChange >= 0))
                  .WithMessage("Return to supplier must reduce stock");

            RuleFor(x => x.Reason).NotEmpty();
        });

        // Customer return Validation
        When(x => x.TransactionType == TransactionType.CustomerReturn, () =>
        {
            RuleFor(x => x.OrderId).NotNull().NotEqual(Guid.Empty);

            RuleFor(x => x.Items)
              .Must(items => items.All(g => g.QuantityChange <= 0))
                  .WithMessage("Customer return must increase stock");
        });

        // Customer return Validation
        When(x => x.TransactionType == TransactionType.Consumption, () =>
        {
            RuleFor(x => x.OrderId).Null();

            RuleFor(x => x.Items)
              .Must(items => items.All(g => g.QuantityChange >= 0))
                  .WithMessage("Consumption must reduce stock");

            RuleFor(x => x.Reason).NotEmpty();
        });

        // Adjustment Validation
        When(x => x.TransactionType == TransactionType.Adjustment, () =>
        {
            RuleFor(x => x.OrderId).Null();

            RuleForEach(x => x.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId).NotEmpty();
                items.RuleFor(i => i.QuantityChange).NotEqual(0);
            });

            RuleFor(x => x.Reason).NotEmpty()
            .WithMessage($"Reason is required for transaction type {TransactionType.Adjustment}");
        });

    }
}
