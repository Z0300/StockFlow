using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Suppliers.GetById;
using StockFlow.Application.Suppliers.Shared;
using StockFlow.Application.Transactions.Create;
using StockFlow.Application.Transactions.Get;
using StockFlow.Application.Transactions.GetById;
using StockFlow.Application.Transactions.TransferOut;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Transactions.Enums;
using StockFlow.Domain.Shared;

namespace StockFlow.Api.Endpoints.Transactions;

[Authorize]
[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ISender _sender;

    public TransactionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactions(
       [FromQuery] RequestGetTransactionQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetTransactionQuery(
            request.From,
            request.To,
            request.ProductName,
            request.Type,
            request.Page,
            request.PageSize);

        Result<PagedList<TransactionsReponse>> result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }



    [HttpGet("{transactionId:guid}")]
    public async Task<IActionResult> GetTransaction(Guid transactionId, CancellationToken cancellationToken)
    {
        var query = new GetTransactionByIdQuery(transactionId);

        Result<TransactionResponse> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction(
        [FromBody] CreateTransactionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateTransactionCommand(
            request.WarehouseId,
            request.OrderId,
            request.TransactionType,
            request.Reason,
            [.. request.Items.Select(item => new TransactionItems(
                item.ProductId,
                item.QuantityChange,
                item.UnitCost))]);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetTransaction), new { transactionId = result.Value }, result.Value);
    }

    
}
