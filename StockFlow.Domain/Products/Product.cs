using SharedKernel;
using StockFlow.Domain.Categories;
using StockFlow.Domain.Warehouses;

namespace StockFlow.Domain.Products;

public sealed class Product : Entity
{
    public required string Name { get; set; }
    public required string Sku { get; set; }
    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public Guid WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }
}