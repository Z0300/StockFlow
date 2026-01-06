using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Suppliers.Create;
using StockFlow.Application.Suppliers.Delete;
using StockFlow.Application.Suppliers.Get;
using StockFlow.Application.Suppliers.GetById;
using StockFlow.Application.Suppliers.Shared;
using StockFlow.Application.Suppliers.Update;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Api.Endpoints.Suppliers;

[Authorize]
[ApiController]
[Route("api/suppliers")]
public class SuppliersController : ControllerBase
{
    private readonly ISender _sender;

    public SuppliersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetSuppliers(CancellationToken cancellationToken)
    {
        var query = new GetSupplierQuery();

        Result<IReadOnlyList<SupplierResponse>> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("{supplierId:guid}")]
    public async Task<IActionResult> GetSupplier(Guid supplierId, CancellationToken cancellationToken)
    {
        var query = new GetSupplierByIdQuery(supplierId);

        Result<SupplierResponse> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateSupplierCommand(
             request.Name,
             request.ContactInfo);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return CreatedAtAction(nameof(GetSupplier), new { supplierId = result.Value }, result.Value);
    }

    [HttpPut("{supplierId:guid}")]
    public async Task<IActionResult> UpdateSupplier(Guid supplierId, [FromBody] UpdateSupplierRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateSupplierCommand(
             supplierId,
             request.Name,
             request.ContactInfo);

        Result result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }

    [HttpDelete("{supplierId:guid}")]
    public async Task<IActionResult> DeleteSupplier(Guid supplierId, CancellationToken cancellationToken)
    {
        var command = new DeleteSupplierCommand(supplierId);

        Result result = await _sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
}
