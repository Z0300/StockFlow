using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Abstractions.Clock;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Transfers;

namespace StockFlow.Application.Transactions.DispatchTransfer;

internal sealed class DispatchTransferCommandHandler
    : ICommandHandler<DispatchTransferCommand>
{
    private readonly ITransferRepository _transferRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public DispatchTransferCommandHandler(
        ITransferRepository transferRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _transferRepository = transferRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result> Handle(DispatchTransferCommand request, CancellationToken cancellationToken)
    {
        Transfer? transfer = await _transferRepository.GetByIdAsync(new TransferId(request.TransferId), cancellationToken);

        if (transfer is null)
        {
            return Result.Failure(TransferErrors.NotFound);
        }

        transfer.Dispatch(_dateTimeProvider.UtcNow);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
