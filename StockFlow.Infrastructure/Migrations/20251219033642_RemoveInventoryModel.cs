using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockFlow.Infrastructure.Migrations;

/// <inheritdoc />
public partial class RemoveInventoryModel : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_transactions_inventories_inventory_id",
            table: "transactions");

        migrationBuilder.DropForeignKey(
            name: "fk_transactions_orders_order_id",
            table: "transactions");

        migrationBuilder.DropTable(
            name: "inventories");

        migrationBuilder.RenameColumn(
            name: "inventory_id",
            table: "transactions",
            newName: "warehouse_id");

        migrationBuilder.RenameIndex(
            name: "ix_transactions_inventory_id",
            table: "transactions",
            newName: "ix_transactions_warehouse_id");

        migrationBuilder.AlterColumn<Guid>(
            name: "order_id",
            table: "transactions",
            type: "uuid",
            nullable: true,
            oldClrType: typeof(Guid),
            oldType: "uuid");

        migrationBuilder.AddColumn<Guid>(
            name: "product_id",
            table: "transactions",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.CreateIndex(
            name: "ix_transactions_product_id",
            table: "transactions",
            column: "product_id");

        migrationBuilder.AddForeignKey(
            name: "fk_transactions_orders_order_id",
            table: "transactions",
            column: "order_id",
            principalTable: "orders",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_transactions_products_product_id",
            table: "transactions",
            column: "product_id",
            principalTable: "products",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_transactions_warehouses_warehouse_id",
            table: "transactions",
            column: "warehouse_id",
            principalTable: "warehouses",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_transactions_orders_order_id",
            table: "transactions");

        migrationBuilder.DropForeignKey(
            name: "fk_transactions_products_product_id",
            table: "transactions");

        migrationBuilder.DropForeignKey(
            name: "fk_transactions_warehouses_warehouse_id",
            table: "transactions");

        migrationBuilder.DropIndex(
            name: "ix_transactions_product_id",
            table: "transactions");

        migrationBuilder.DropColumn(
            name: "product_id",
            table: "transactions");

        migrationBuilder.RenameColumn(
            name: "warehouse_id",
            table: "transactions",
            newName: "inventory_id");

        migrationBuilder.RenameIndex(
            name: "ix_transactions_warehouse_id",
            table: "transactions",
            newName: "ix_transactions_inventory_id");

        migrationBuilder.AlterColumn<Guid>(
            name: "order_id",
            table: "transactions",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty,
            oldClrType: typeof(Guid),
            oldType: "uuid",
            oldNullable: true);

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

        migrationBuilder.CreateIndex(
            name: "ix_inventories_product_id",
            table: "inventories",
            column: "product_id");

        migrationBuilder.CreateIndex(
            name: "ix_inventories_warehouse_id",
            table: "inventories",
            column: "warehouse_id");

        migrationBuilder.AddForeignKey(
            name: "fk_transactions_inventories_inventory_id",
            table: "transactions",
            column: "inventory_id",
            principalTable: "inventories",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_transactions_orders_order_id",
            table: "transactions",
            column: "order_id",
            principalTable: "orders",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}
