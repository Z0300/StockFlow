using FluentValidation;

namespace StockFlow.Application.Orders.Cancel;

internal sealed class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty();
    }
}
