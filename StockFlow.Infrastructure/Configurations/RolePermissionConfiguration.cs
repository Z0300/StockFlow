using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Auth;
using Permission = StockFlow.Domain.Enums.Permission;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class RolePermissionConfiguration
     : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasData(
                // All permissions for Admin
            Enum.GetValues<Permission>()
                .Select(p => Create(Role.Admin, p))
                // Inventory Manager permissions
                .Concat([
                    Create(Role.InventoryManager, Permission.InventoryView),
                    Create(Role.InventoryManager, Permission.InventoryCreate),
                    Create(Role.InventoryManager, Permission.InventoryUpdate),
                    Create(Role.InventoryManager, Permission.InventoryDelete),
                    Create(Role.InventoryManager, Permission.InventoryAdjustStock),
                    Create(Role.InventoryManager, Permission.InventoryTransferStock),
                    Create(Role.InventoryManager, Permission.InventoryViewCost),
                    Create(Role.InventoryManager, Permission.InventoryUpdateCost),
                    Create(Role.InventoryManager, Permission.InventoryViewValuation),
                    Create(Role.InventoryManager, Permission.InventoryCreateAdjustment),
                    Create(Role.InventoryManager, Permission.InventoryViewAdjustment),
                    Create(Role.InventoryManager, Permission.InventoryApproveAdjustment),
                    Create(Role.InventoryManager, Permission.InventoryRejectAdjustment),
                    Create(Role.InventoryManager, Permission.ReportViewInventory),
                    Create(Role.InventoryManager, Permission.ReportViewMovement),
                    Create(Role.InventoryManager, Permission.ReportViewValuation)
                ])
                // Warehouse Staff permissions
                .Concat([
                    Create(Role.WarehouseStaff, Permission.InventoryView),
                    Create(Role.WarehouseStaff, Permission.InventoryAdjustStock),
                    Create(Role.WarehouseStaff, Permission.InventoryReserveStock),
                    Create(Role.WarehouseStaff, Permission.InventoryReleaseStock),
                    Create(Role.WarehouseStaff, Permission.InventoryCreateAdjustment),
                    Create(Role.WarehouseStaff, Permission.InventoryViewAdjustment),
                    Create(Role.WarehouseStaff, Permission.WarehouseView)
                    ])
                // Auditor permissions
                .Concat([
                    Create(Role.Auditor, Permission.InventoryView),
                    Create(Role.Auditor, Permission.InventoryViewCost),
                    Create(Role.Auditor, Permission.InventoryViewValuation),
                    Create(Role.Auditor, Permission.ReportViewInventory),
                    Create(Role.Auditor, Permission.ReportViewMovement),
                    Create(Role.Auditor, Permission.ReportViewValuation),
                    Create(Role.Auditor, Permission.ReportExport),
                    Create(Role.Auditor, Permission.AuditViewLogs),
                    Create(Role.Auditor, Permission.AuditExportLogs)
                    ])
                // Procurement permissions
                .Concat([      
                    Create(Role.Procurement, Permission.ProcurementViewSupplier),
                    Create(Role.Procurement, Permission.ProcurementCreateSupplier),
                    Create(Role.Procurement, Permission.ProcurementUpdateSupplier),
                    Create(Role.Procurement, Permission.ProcurementCreatePurchaseOrder),
                    Create(Role.Procurement, Permission.ProcurementUpdatePurchaseOrder),
                    Create(Role.Procurement, Permission.ProcurementApprovePurchaseOrder),
                    Create(Role.Procurement, Permission.ProcurementReceiveGoods),
                    Create(Role.Procurement, Permission.InventoryView)
                    ])
                // System Integration permissions
                .Concat([
                    Create(Role.SystemIntegration, Permission.IntegrationInventorySync),
                    Create(Role.SystemIntegration, Permission.IntegrationInventoryAdjust),
                    Create(Role.SystemIntegration, Permission.IntegrationInventoryExport),
                    Create(Role.SystemIntegration, Permission.InventoryView),
                    Create(Role.SystemIntegration, Permission.InventoryAdjustStock)
                    ])
            );
    }

    private static RolePermission Create(
       Role role, Permission permission)
    {
        return new RolePermission
        {
            RoleId = role.Id,
            PermissionId = (int)permission
        };
    }
}
