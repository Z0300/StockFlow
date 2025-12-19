namespace StockFlow.Domain.Enums;

public enum TransactionType
{
    OpeningBalance = 1,

    PurchaseReceipt = 10,
    CustomerReturn = 11,

    SaleIssue = 20,
    Consumption = 21,
    ReturnToSupplier = 22,

    TransferOut = 30,
    TransferIn = 31,

    Adjustment = 40
}
