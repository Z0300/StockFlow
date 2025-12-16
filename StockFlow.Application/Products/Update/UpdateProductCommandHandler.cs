using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Products;

namespace StockFlow.Application.Products.Update;

internal sealed class UpdateProductCommandHandler(IApplicationDbContext context)
    : ICommandHandler<UpdateProductCommand>
{
    public async Task<Result> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        Product? product = await context.Products
            .SingleOrDefaultAsync(x => x.Id == command.ProductId, cancellationToken);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(command.ProductId));
        }

        product.Name = command.Name;
        product.Price = command.Price;
        product.CategoryId = command.CategoryId ?? Guid.Empty;
        product.WarehouseId = command.WarehouseId ?? Guid.Empty;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
