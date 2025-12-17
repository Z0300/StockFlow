using FluentValidation;

namespace StockFlow.Application.Orders.Create;

internal sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.WarehouseId)
            .NotEmpty();
        RuleFor(x => x.SupplierId)
            .NotEmpty();
        RuleFor(x => x.Items)
            .NotEmpty();

        RuleForEach(x => x.Items)
            .ChildRules(items =>
        {
            items.RuleFor(i => i.ProductId)
                .NotEmpty();
            items.RuleFor(i => i.Quantity)
                .GreaterThan(0);
            items.RuleFor(i => i.UnitPrice)
                .GreaterThan(0);
        });
    }
}
