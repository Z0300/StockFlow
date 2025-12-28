using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Warehouses.Create;
using StockFlow.Application.Warehouses.Delete;
using StockFlow.Application.Warehouses.Get;
using StockFlow.Application.Warehouses.GetById;
using StockFlow.Application.Warehouses.Shared;
using StockFlow.Application.Warehouses.Update;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Api.Endpoints.Warehouses;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private readonly ISender _sender;

    public WarehouseController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetWarehouses(CancellationToken cancellationToken)
    {
        var query = new GetWarehouseQuery();

        Result<IReadOnlyList<WarehouseResponse>> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("{warehouseId:guid}")]
    public async Task<IActionResult> GetWarehouse(Guid warehouseId, CancellationToken cancellationToken)
    {
        var query = new GetWarehouseByIdQuery(warehouseId);
        Result<WarehouseResponse> result = await _sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWarehouse([FromBody] CreateWarehouseRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateWarehouseCommand(
             request.Name,
             request.Location);

        Result<Guid> result = await _sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return CreatedAtAction(nameof(GetWarehouse), new { id = result.Value }, result.Value);
    }

    [HttpPut("{warehouseId:guid}")]
    public async Task<IActionResult> UpdateWarehouse(Guid warehouseId, [FromBody] UpdateWarehouseRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateWarehouseCommand(
             warehouseId,
             request.Name,
             request.Location);

        Result result = await _sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return NoContent();
    }

    [HttpDelete("{warehouseId:guid}")]
    public async Task<IActionResult> DeleteWarehouse(Guid warehouseId, CancellationToken cancellationToken)
    {
        var command = new DeleteWarehouseCommand(warehouseId);
        Result result = await _sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return NoContent();
    }
}
