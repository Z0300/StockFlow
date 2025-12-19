using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockFlow.Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitialModels : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "categories",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_categories", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "permissions",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_permissions", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "roles",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_roles", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "suppliers",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                contact_info = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_suppliers", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "users",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                first_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                last_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                email = table.Column<string>(type: "text", nullable: false),
                password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_users", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "warehouses",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                location = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_warehouses", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "products",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                sku = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                category_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_products", x => x.id);
                table.ForeignKey(
                    name: "fk_products_categories_category_id",
                    column: x => x.category_id,
                    principalTable: "categories",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "role_permissions",
            columns: table => new
            {
                role_id = table.Column<int>(type: "integer", nullable: false),
                permission_id = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_role_permissions", x => new { x.role_id, x.permission_id });
                table.ForeignKey(
                    name: "fk_role_permissions_permissions_permission_id",
                    column: x => x.permission_id,
                    principalTable: "permissions",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_role_permissions_roles_role_id",
                    column: x => x.role_id,
                    principalTable: "roles",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "role_user",
            columns: table => new
            {
                role_id = table.Column<int>(type: "integer", nullable: false),
                users_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_role_user", x => new { x.role_id, x.users_id });
                table.ForeignKey(
                    name: "fk_role_user_roles_role_id",
                    column: x => x.role_id,
                    principalTable: "roles",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_role_user_users_users_id",
                    column: x => x.users_id,
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "orders",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                warehouse_id = table.Column<Guid>(type: "uuid", nullable: false),
                supplier_id = table.Column<Guid>(type: "uuid", nullable: false),
                order_status = table.Column<int>(type: "integer", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                received_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_orders", x => x.id);
                table.ForeignKey(
                    name: "fk_orders_suppliers_supplier_id",
                    column: x => x.supplier_id,
                    principalTable: "suppliers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_orders_warehouses_warehouse_id",
                    column: x => x.warehouse_id,
                    principalTable: "warehouses",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "inventories",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                warehouse_id = table.Column<Guid>(type: "uuid", nullable: false),
                quantity = table.Column<int>(type: "integer", nullable: false),
                updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_inventories", x => x.id);
                table.ForeignKey(
                    name: "fk_inventories_products_product_id",
                    column: x => x.product_id,
                    principalTable: "products",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_inventories_warehouses_warehouse_id",
                    column: x => x.warehouse_id,
                    principalTable: "warehouses",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "order_items",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                quantity = table.Column<int>(type: "integer", nullable: false),
                unit_price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                order_id = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_order_items", x => x.id);
                table.ForeignKey(
                    name: "fk_order_items_orders_order_id",
                    column: x => x.order_id,
                    principalTable: "orders",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "fk_order_items_products_product_id",
                    column: x => x.product_id,
                    principalTable: "products",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "transactions",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                transaction_group_id = table.Column<Guid>(type: "uuid", nullable: false),
                inventory_id = table.Column<Guid>(type: "uuid", nullable: false),
                quantity_change = table.Column<int>(type: "integer", nullable: false),
                transaction_type = table.Column<int>(type: "integer", nullable: false),
                order_id = table.Column<Guid>(type: "uuid", nullable: false),
                unit_cost = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                reason = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_transactions", x => x.id);
                table.ForeignKey(
                    name: "fk_transactions_inventories_inventory_id",
                    column: x => x.inventory_id,
                    principalTable: "inventories",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_transactions_orders_order_id",
                    column: x => x.order_id,
                    principalTable: "orders",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "permissions",
            columns: ["id", "name"],
            values: new object[,]
            {
                { 1000, "InventoryView" },
                { 1001, "InventoryCreate" },
                { 1002, "InventoryUpdate" },
                { 1003, "InventoryDelete" },
                { 1004, "InventoryAdjustStock" },
                { 1005, "InventoryReserveStock" },
                { 1006, "InventoryReleaseStock" },
                { 1007, "InventoryTransferStock" },
                { 1008, "InventoryViewCost" },
                { 1009, "InventoryUpdateCost" },
                { 1010, "InventoryViewValuation" },
                { 1200, "InventoryCreateAdjustment" },
                { 1201, "InventoryViewAdjustment" },
                { 1202, "InventoryApproveAdjustment" },
                { 1203, "InventoryRejectAdjustment" },
                { 1300, "WarehouseView" },
                { 1301, "WarehouseCreate" },
                { 1302, "WarehouseUpdate" },
                { 1303, "WarehouseDelete" },
                { 1304, "WarehouseAssignUser" },
                { 1305, "WarehouseTransferInventory" },
                { 1400, "ProcurementViewSupplier" },
                { 1401, "ProcurementCreateSupplier" },
                { 1402, "ProcurementUpdateSupplier" },
                { 1403, "ProcurementCreatePurchaseOrder" },
                { 1404, "ProcurementUpdatePurchaseOrder" },
                { 1405, "ProcurementApprovePurchaseOrder" },
                { 1406, "ProcurementReceiveGoods" },
                { 1500, "ReportViewInventory" },
                { 1501, "ReportViewMovement" },
                { 1502, "ReportViewValuation" },
                { 1503, "ReportExport" },
                { 1504, "AuditViewLogs" },
                { 1505, "AuditExportLogs" },
                { 1600, "UserView" },
                { 1601, "UserCreate" },
                { 1602, "UserUpdate" },
                { 1603, "UserDeactivate" },
                { 1604, "RoleView" },
                { 1605, "RoleCreate" },
                { 1606, "RoleUpdate" },
                { 1607, "PermissionAssign" },
                { 1608, "PermissionRevoke" },
                { 1700, "IntegrationInventorySync" },
                { 1701, "IntegrationInventoryAdjust" },
                { 1702, "IntegrationInventoryExport" },
                { 1703, "IntegrationWebhookReceive" },
                { 1800, "SystemViewSettings" },
                { 1801, "SystemUpdateSettings" }
            });

        migrationBuilder.InsertData(
            table: "roles",
            columns: ["id", "name"],
            values: new object[,]
            {
                { 1, "Admin" },
                { 2, "InventoryManager" },
                { 3, "WarehouseStaff" },
                { 4, "Auditor" },
                { 5, "Procurement" },
                { 6, "SystemIntegration " }
            });

        migrationBuilder.InsertData(
            table: "role_permissions",
            columns: ["permission_id", "role_id"],
            values: new object[,]
            {
                { 1000, 1 },
                { 1001, 1 },
                { 1002, 1 },
                { 1003, 1 },
                { 1004, 1 },
                { 1005, 1 },
                { 1006, 1 },
                { 1007, 1 },
                { 1008, 1 },
                { 1009, 1 },
                { 1010, 1 },
                { 1200, 1 },
                { 1201, 1 },
                { 1202, 1 },
                { 1203, 1 },
                { 1300, 1 },
                { 1301, 1 },
                { 1302, 1 },
                { 1303, 1 },
                { 1304, 1 },
                { 1305, 1 },
                { 1400, 1 },
                { 1401, 1 },
                { 1402, 1 },
                { 1403, 1 },
                { 1404, 1 },
                { 1405, 1 },
                { 1406, 1 },
                { 1500, 1 },
                { 1501, 1 },
                { 1502, 1 },
                { 1503, 1 },
                { 1504, 1 },
                { 1505, 1 },
                { 1600, 1 },
                { 1601, 1 },
                { 1602, 1 },
                { 1603, 1 },
                { 1604, 1 },
                { 1605, 1 },
                { 1606, 1 },
                { 1607, 1 },
                { 1608, 1 },
                { 1700, 1 },
                { 1701, 1 },
                { 1702, 1 },
                { 1703, 1 },
                { 1800, 1 },
                { 1801, 1 },
                { 1000, 2 },
                { 1001, 2 },
                { 1002, 2 },
                { 1003, 2 },
                { 1004, 2 },
                { 1007, 2 },
                { 1008, 2 },
                { 1009, 2 },
                { 1010, 2 },
                { 1200, 2 },
                { 1201, 2 },
                { 1202, 2 },
                { 1203, 2 },
                { 1500, 2 },
                { 1501, 2 },
                { 1502, 2 },
                { 1000, 3 },
                { 1004, 3 },
                { 1005, 3 },
                { 1006, 3 },
                { 1200, 3 },
                { 1201, 3 },
                { 1300, 3 },
                { 1000, 4 },
                { 1008, 4 },
                { 1010, 4 },
                { 1500, 4 },
                { 1501, 4 },
                { 1502, 4 },
                { 1503, 4 },
                { 1504, 4 },
                { 1505, 4 },
                { 1000, 5 },
                { 1400, 5 },
                { 1401, 5 },
                { 1402, 5 },
                { 1403, 5 },
                { 1404, 5 },
                { 1405, 5 },
                { 1406, 5 },
                { 1000, 6 },
                { 1004, 6 },
                { 1700, 6 },
                { 1701, 6 },
                { 1702, 6 }
            });

        migrationBuilder.CreateIndex(
            name: "ix_categories_name",
            table: "categories",
            column: "name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_inventories_product_id",
            table: "inventories",
            column: "product_id");

        migrationBuilder.CreateIndex(
            name: "ix_inventories_warehouse_id",
            table: "inventories",
            column: "warehouse_id");

        migrationBuilder.CreateIndex(
            name: "ix_order_items_order_id",
            table: "order_items",
            column: "order_id");

        migrationBuilder.CreateIndex(
            name: "ix_order_items_product_id",
            table: "order_items",
            column: "product_id");

        migrationBuilder.CreateIndex(
            name: "ix_orders_supplier_id",
            table: "orders",
            column: "supplier_id");

        migrationBuilder.CreateIndex(
            name: "ix_orders_warehouse_id",
            table: "orders",
            column: "warehouse_id");

        migrationBuilder.CreateIndex(
            name: "ix_products_category_id",
            table: "products",
            column: "category_id");

        migrationBuilder.CreateIndex(
            name: "ix_role_permissions_permission_id",
            table: "role_permissions",
            column: "permission_id");

        migrationBuilder.CreateIndex(
            name: "ix_role_user_users_id",
            table: "role_user",
            column: "users_id");

        migrationBuilder.CreateIndex(
            name: "ix_transactions_inventory_id",
            table: "transactions",
            column: "inventory_id");

        migrationBuilder.CreateIndex(
            name: "ix_transactions_order_id",
            table: "transactions",
            column: "order_id");

        migrationBuilder.CreateIndex(
            name: "ix_transactions_transaction_group_id",
            table: "transactions",
            column: "transaction_group_id");

        migrationBuilder.CreateIndex(
            name: "ix_users_email",
            table: "users",
            column: "email",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "order_items");

        migrationBuilder.DropTable(
            name: "role_permissions");

        migrationBuilder.DropTable(
            name: "role_user");

        migrationBuilder.DropTable(
            name: "transactions");

        migrationBuilder.DropTable(
            name: "permissions");

        migrationBuilder.DropTable(
            name: "roles");

        migrationBuilder.DropTable(
            name: "users");

        migrationBuilder.DropTable(
            name: "inventories");

        migrationBuilder.DropTable(
            name: "orders");

        migrationBuilder.DropTable(
            name: "products");

        migrationBuilder.DropTable(
            name: "suppliers");

        migrationBuilder.DropTable(
            name: "warehouses");

        migrationBuilder.DropTable(
            name: "categories");
    }
}
