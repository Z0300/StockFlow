using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.DomainErrors;
using StockFlow.Domain.Entities;
using StockFlow.Domain.Enums;
using StockFlow.Domain.Exceptions;


namespace StockFlow.Application.Transactions.TransferOut;

internal sealed class TransferOutCommandHandler(
    IApplicationDbContext context)
    : ICommandHandler<TransferOutCommand, Guid>
{
    public async Task<Result<Guid>> Handle(TransferOutCommand command, CancellationToken cancellationToken)
    {

        foreach (TransferOutItems item in command.Items)
        {
            int availableQuantity = context.Transactions
                .Where(stock => stock.WarehouseId == command.SourceWarehouseId &&
                        stock.ProductId == item.ProductId)
                .Sum(stock => stock.QuantityChange);

            if (availableQuantity < item.RequestedQuantity)
            {
                return Result.Failure<Guid>(TransferErrors.InsufficientStock(item.ProductId));
            }
        }

        var transfer =
            new Transfer
            {
                Id = Guid.NewGuid(),
                DestinationWarehouseId = command.DestinationWarehouseId,
                SourceWarehouseId = command.SourceWarehouseId,
                Status = TransferStatus.Draft,
                Items = [.. command.Items.Select(item => new TransferItem
                {
                    ProductId = item.ProductId,
                    RequestedQuantity = item.RequestedQuantity,
                    ReceivedQuantity = 0,
                })],
            };

        context.Transfers.Add(transfer);
        await context.SaveChangesAsync(cancellationToken);

        return transfer.Id;
    }
}
