using FluentValidation;

namespace StockFlow.Application.Transactions.Create;

internal sealed class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {

        RuleForEach(x => x.Items).ChildRules(items =>
        {

            items.RuleFor(i => i.ProductId)
                .NotEmpty();

            items.RuleFor(i => i.WarehouseId)
               .NotEmpty();

            items.RuleFor(i => i.TransactionType)
               .IsInEnum();

            items.RuleFor(i => i.QuantityChange)
                .GreaterThan(0);

            items.RuleFor(i => i.OrderId)
               .NotEmpty();
        });
    }
}
