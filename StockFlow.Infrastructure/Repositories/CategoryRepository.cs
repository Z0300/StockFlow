using Microsoft.EntityFrameworkCore;
using StockFlow.Domain.Entities.Categories;
using StockFlow.Infrastructure.Database;

namespace StockFlow.Infrastructure.Repositories;

internal sealed class CategoryRepository : Repository<Category, CategoryId>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<bool> IsNameUnique(string name, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Category>()
            .AnyAsync(category => category.Name == name, cancellationToken);
    }
}
