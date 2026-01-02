namespace StockFlow.Application.Suppliers.Shared;

public sealed class SupplierResponse
{
    public Guid SupplierId { get; init; }
    public string SupplierName { get; init; }
    public string SupplierContactInfo { get; init; }
}
