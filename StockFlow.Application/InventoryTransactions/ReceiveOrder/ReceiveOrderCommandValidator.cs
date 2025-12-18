using FluentValidation;

namespace StockFlow.Application.InventoryTransactions.ReceiveOrder;

internal sealed class ReceiveOrderCommandValidator : AbstractValidator<ReceiveOrderCommand>
{
    public ReceiveOrderCommandValidator()
    {

        RuleForEach(x => x.Items).ChildRules(items =>
        {
            items.RuleFor(i => i.ProductId)
                .NotEmpty();

            items.RuleFor(i => i.QuantityChange)
                .GreaterThan(0);

            items.RuleFor(i => i.OrderId)
               .NotEmpty();
        });
    }
}
