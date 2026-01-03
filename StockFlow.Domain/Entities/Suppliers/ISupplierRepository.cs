namespace StockFlow.Domain.Entities.Suppliers;

public interface ISupplierRepository
{
    Task<Supplier> GetByIdAsync(SupplierId id, CancellationToken cancellationToken = default);
    Task<bool> IsNameUnique(string name, CancellationToken cancellationToken = default);
    void Add(Supplier supplier);
    void Remove(Supplier supplier);
}
