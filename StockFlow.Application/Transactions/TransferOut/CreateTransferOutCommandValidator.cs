using FluentValidation;

namespace StockFlow.Application.Transactions.TransferOut;

internal sealed class CreateTransferOutCommandValidator : AbstractValidator<CreateTransferOutCommand>
{
    public CreateTransferOutCommandValidator()
    {
        RuleFor(x => x.Items)
           .NotEmpty()
           .WithMessage("At least one item is required.");

        RuleFor(x => x.SourceWarehouseId)
           .NotEmpty();

        RuleFor(x => x.DestinationWarehouseId)
         .NotEmpty();

        RuleFor(x => x.Items)
           .Must(items =>
               items.GroupBy(i => i.ProductId)
                    .All(g => g.Count() == 1))
           .WithMessage("Duplicate products in transfer are not allowed");


        RuleFor(x => x)
            .Must(x => x.SourceWarehouseId != x.DestinationWarehouseId)
            .WithMessage("Source and destination warehouses must be different.");

        RuleForEach(x => x.Items).ChildRules(items =>
        {
            items.RuleFor(i => i.ProductId)
                .NotEmpty();

            items.RuleFor(i => i.RequestedQuantity)
                .NotEqual(0);

        });
    }
}


