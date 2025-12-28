using Microsoft.EntityFrameworkCore;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Infrastructure.Database;

namespace StockFlow.Infrastructure.Repositories;

internal abstract class Repository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : class
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext) => DbContext = dbContext;

    public async Task<TEntity> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default)
    {
        return await DbContext
         .Set<TEntity>()
         .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);

    }

    public virtual void Add(TEntity entity)
        => DbContext.Add(entity);

    public virtual void Remove(TEntity entity)
        => DbContext.Set<TEntity>().Remove(entity);
}
