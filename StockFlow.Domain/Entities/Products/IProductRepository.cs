namespace StockFlow.Domain.Entities.Products;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(ProductId id, CancellationToken cancellationToken = default);
    Task<bool> IsNameUnique(string name, CancellationToken cancellationToken = default);
    void Add(Product product);
    void Remove(Product product);
}
