namespace StockFlow.Domain.Entities.Warehouses;

public interface IWarehouseRepository
{
    Task<Warehouse> GetByIdAsync(WarehouseId id, CancellationToken cancellationToken = default);
    Task<bool> IsNameUnique(string name, CancellationToken cancellationToken = default);
    void Add(Warehouse category);
    void Remove(Warehouse category);
}
