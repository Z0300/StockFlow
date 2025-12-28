using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Categories.Delete;
using StockFlow.Application.Categories.Update;
using StockFlow.Application.Orders.Create;
using StockFlow.Application.Products.Create;
using StockFlow.Application.Products.Delete;
using StockFlow.Application.Products.Get;
using StockFlow.Application.Products.GetById;
using StockFlow.Application.Products.Update;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Api.Endpoints.Products;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ISender _sender;

    public ProductsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var query = new GetProductQuery();

        Result<IReadOnlyList<ProductsResponse>> result = await _sender.Send(query, cancellationToken);
       
        return Ok(result.Value);
    }

    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> GetProduct(Guid productId, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(productId);
        Result<ProductResponse> result = await _sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(
             request.Name,
             request.Sku,
             request.Price,
             request.CategoryId);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return CreatedAtAction(nameof(GetProduct), new { id = result.Value }, result.Value);
    }

    [HttpPut("{productId:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(
                     productId,
                     request.Name,
                     request.Sku,
                     request.Price,
                     request.CategoryId);

        Result result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }

    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid productId, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(productId);

        Result result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
}
