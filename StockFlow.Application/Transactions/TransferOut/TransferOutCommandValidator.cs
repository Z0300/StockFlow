using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Enums;

namespace StockFlow.Application.Transactions.TransferOut;

internal sealed class TransferOutCommandValidator : AbstractValidator<TransferOutCommand>
{
    public TransferOutCommandValidator()
    {
        RuleFor(x => x.Items)
           .NotEmpty()
           .WithMessage("At least one item is required.");

        RuleFor(x => x.SourceWarehouseId)
           .NotEmpty();

        RuleFor(x => x.DestinationWarehouseId)
         .NotEmpty();

        RuleFor(x => x.Status)
          .IsInEnum();

        RuleForEach(x => x.Items).ChildRules(items =>
        {
            items.RuleFor(i => i.ProductId)
                .NotEmpty();

            items.RuleFor(i => i.RequestedQuantity)
                .NotEqual(0);

            items.RuleFor(i => i.ReceivedQuantity)
                .NotEqual(0);
        });
    }
}


