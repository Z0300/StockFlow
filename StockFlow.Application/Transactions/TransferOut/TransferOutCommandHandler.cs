using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities;
using StockFlow.Domain.Exceptions;


namespace StockFlow.Application.Transactions.TransferOut;

internal sealed class TransferOutCommandHandler(
    IApplicationDbContext context)
    : ICommandHandler<TransferOutCommand, Guid>
{
    public async Task<Result<Guid>> Handle(TransferOutCommand command, CancellationToken cancellationToken)
    {
        var transferId = Guid.NewGuid();

        List<Transfer> transferItems = [.. command.Items.Select(item =>
            new Transfer
            {
                Id = transferId,
                DestinationWarehouseId = command.DestinationWarehouseId,
                SourceWarehouseId = command.SourceWarehouseId,
                Status = command.Status,
                Items=
                [
                    new() {
                        ProductId = item.ProductId,
                        RequestedQuantity = item.RequestedQuantity,
                        ReceivedQuantity = item.ReceivedQuantity
                    }
                ],
            })];

        await context.Transfers.BulkInsertOptimizedAsync(transferItems,
              options => options.IncludeGraph = true);

        return transferId;
    }
}
