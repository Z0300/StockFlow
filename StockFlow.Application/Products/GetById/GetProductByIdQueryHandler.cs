using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Categories.Shared;
using StockFlow.Application.Warehouses.Shared;
using StockFlow.Domain.Products;

namespace StockFlow.Application.Products.GetById;

internal sealed class GetProductByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    public async Task<Result<GetProductByIdResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        GetProductByIdResponse? product = await context.Products
            .AsNoTracking()
            .Where(p => p.Id == query.ProductId)
            .Select(product => new GetProductByIdResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = new CategoryResponse
                {
                    Id = product.Category != null ? product.Category.Id : Guid.Empty,
                    Name = product.Category != null ? product.Category.Name : string.Empty,
                    Description = product.Category != null ? product.Category.Description : string.Empty
                },
                Warehouse = new WarehouseResponse
                {
                    Id = product.Warehouse != null ? product.Warehouse.Id : Guid.Empty,
                    Name = product.Warehouse != null ? product.Warehouse.Name : string.Empty,
                    Location = product.Warehouse != null ? product.Warehouse.Location : string.Empty
                }
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            return Result.Failure<GetProductByIdResponse>(ProductErrors.NotFound(query.ProductId));
        }

        return product;
    }
}
