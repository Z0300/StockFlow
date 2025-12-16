using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Products;

namespace StockFlow.Application.Products.Delete;

internal sealed class DeleteProductCommandHandler(IApplicationDbContext context)
    : ICommandHandler<DeleteProductCommand>
{
    public async Task<Result> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        Product? product = await context.Products
            .SingleOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(command.ProductId));
        }

        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
