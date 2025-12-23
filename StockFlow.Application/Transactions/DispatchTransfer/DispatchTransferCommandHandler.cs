using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.DomainErrors;
using StockFlow.Domain.DomainEvents;
using StockFlow.Domain.Entities;
using StockFlow.Domain.Enums;

namespace StockFlow.Application.Transactions.DispatchTransfer;

internal sealed class DispatchTransferCommandHandler(IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<DispatchTransferCommand>
{
    public async Task<Result> Handle(DispatchTransferCommand command, CancellationToken cancellationToken)
    {
        Transfer? transfer = await context.Transfers
            .SingleOrDefaultAsync(t => t.Id == command.TransferId, cancellationToken);

        if (transfer is null)
        {
            return Result.Failure(TransferErrors.TransferNotFound(command.TransferId));
        }

        if (transfer.Status != TransferStatus.Draft)
        {
            return Result.Failure(TransferErrors.TransferAlreadyDispatched(command.TransferId));
        }

        transfer.Status = TransferStatus.InTransit;
        transfer.DispatchAt = dateTimeProvider.UtcNow;

        transfer.Raise(new DispatchTransferDomainEvent(transfer.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
