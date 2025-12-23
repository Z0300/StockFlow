using FluentValidation;

namespace StockFlow.Application.Transactions.DispatchTransfer;

internal sealed class DispatchTransferCommandValidator : AbstractValidator<DispatchTransferCommand>
{
    public DispatchTransferCommandValidator()
    {
        RuleFor(x => x.TransferId)
           .NotEmpty();
    }
}
