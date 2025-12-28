namespace StockFlow.Application.Suppliers.Shared;

public sealed class SupplierResponse
{
    public Guid SupplierId { get; set; }
    public string SupplierName { get; set; }
    public string SupplierContactInfo { get; set; }
}
