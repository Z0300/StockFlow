using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Products;

namespace StockFlow.Application.Products.Create;

internal sealed class CreateProductCommandHandler(IApplicationDbContext context)
    : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        if (await context.Products.AnyAsync(p => p.Name == command.Name, cancellationToken))
        {
            return Result.Failure<Guid>(ProductErrors.NameNotUnique);
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Sku = command.Sku,
            Price = command.Price,
            CategoryId = command.CategoryId
        };

        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
