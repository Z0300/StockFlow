namespace StockFlow.Domain.Enums;

public enum Permission
{
    // Inventory

    InventoryView = 1000,
    InventoryCreate,
    InventoryUpdate,
    InventoryDelete,
    InventoryAdjustStock,
    InventoryReserveStock,
    InventoryReleaseStock,
    InventoryTransferStock,
    InventoryViewCost,
    InventoryUpdateCost,
    InventoryViewValuation,

    // Adjustments & Approval

    InventoryCreateAdjustment = 1200,
    InventoryViewAdjustment,
    InventoryApproveAdjustment,
    InventoryRejectAdjustment,

    // Warehouse / Location 
    WarehouseView = 1300,
    WarehouseCreate,
    WarehouseUpdate,
    WarehouseDelete,
    WarehouseAssignUser,
    WarehouseTransferInventory,

    // Procurement
    ProcurementViewSupplier = 1400,
    ProcurementCreateSupplier,
    ProcurementUpdateSupplier,
    ProcurementCreatePurchaseOrder,
    ProcurementUpdatePurchaseOrder,
    ProcurementApprovePurchaseOrder,
    ProcurementReceiveGoods,

    // Reports & Audit 
    ReportViewInventory = 1500,
    ReportViewMovement,
    ReportViewValuation,
    ReportExport,
    AuditViewLogs,
    AuditExportLogs,

    // User & Access Management
    UserView = 1600,
    UserCreate,
    UserUpdate,
    UserDeactivate,
    RoleView,
    RoleCreate,
    RoleUpdate,
    PermissionAssign,
    PermissionRevoke,

    // System / Integration 
    IntegrationInventorySync = 1700,
    IntegrationInventoryAdjust,
    IntegrationInventoryExport,
    IntegrationWebhookReceive,

    // System Configuration 
    SystemViewSettings = 1800,
    SystemUpdateSettings
}
