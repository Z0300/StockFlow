using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Transactions.DispatchTransfer;
using StockFlow.Application.Transactions.GetTransfer;
using StockFlow.Application.Transactions.GetTransferById;
using StockFlow.Application.Transactions.TransferOut;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Api.Endpoints.Transfers;

[Authorize]
[ApiController]
[Route("api/transfers")]
public class TransfersController : ControllerBase
{
    private readonly ISender _sender;

    public TransfersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetTransfers(CancellationToken cancellationToken)
    {
        var query = new GetTransferQuery();

        Result<IReadOnlyCollection<TransfersResponse>> result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("{transferId:guid}")]
    public async Task<IActionResult> GetTransfer(Guid transferId, CancellationToken cancellationToken)
    {
        var query = new GetTransferByIdQuery(transferId);

        Result<TransferResponse> result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransfer(
      [FromBody] CreateTransferRequest request,
      CancellationToken cancellationToken)
    {
        var command = new CreateTransferOutCommand(
            request.SourceWarehouseId,
            request.DestinationWarehouseId,
            [..request.Items.Select(i => new TransferOutItems(
                i.ProductId,
                i.RequestedQuantity
            ))]);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetTransfer), new { transferId = result.Value }, result.Value);
    }

    [HttpPut("{transferId:guid}")]
    public async Task<IActionResult> Confirm(
    Guid transferId, CancellationToken cancellationToken)
    {
        var command = new DispatchTransferCommand(transferId);

        Result result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
}
