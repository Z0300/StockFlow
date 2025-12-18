using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Products.Get;

internal sealed class GetProductQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetProductQuery, List<GetProductResponse>>
{
    public async Task<Result<List<GetProductResponse>>> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        List<GetProductResponse> products = await context.Products
            .AsNoTracking()
            .Select(product => new GetProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category != null ? product.Category.Name : string.Empty
            })
            .ToListAsync(cancellationToken);

        return products;
    }
}
