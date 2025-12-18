namespace StockFlow.Domain.Enums;

public enum TransactionType
{
    OpeningBalance = 1,
    PurchaseReceipt = 2,
    SaleIssue = 3,
    TransferIn = 4,
    TransferOut = 5,
    Adjustment = 6,
    ReturnToSupplier = 7
}
