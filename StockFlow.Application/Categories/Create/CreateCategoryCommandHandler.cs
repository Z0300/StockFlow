using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Categories;

namespace StockFlow.Application.Categories.Create;

internal sealed class CreateCategoryCommandHandler(
    IApplicationDbContext context)
    : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        if (await context.Categories.AnyAsync(c => c.Name == command.Name, cancellationToken))
        {
            return Result.Failure<Guid>(CategoryErrors.NameNotUnique);
        }

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description
        };

        context.Categories.Add(category);
        await context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
