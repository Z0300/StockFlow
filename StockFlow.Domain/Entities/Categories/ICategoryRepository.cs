namespace StockFlow.Domain.Entities.Categories;

public interface ICategoryRepository
{
    Task<Category> GetByIdAsync(CategoryId id, CancellationToken cancellationToken = default);
    Task<bool> IsNameUnique(string name, CancellationToken cancellationToken = default);
    void Add(Category category);
    void Remove(Category category);
}
