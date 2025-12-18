using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockFlow.Infrastructure.Migrations;

/// <inheritdoc />
public partial class RemoveWarehouseInProduct : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_products_warehouses_warehouse_id",
            table: "products");

        migrationBuilder.DropIndex(
            name: "ix_products_warehouse_id",
            table: "products");

        migrationBuilder.DropColumn(
            name: "warehouse_id",
            table: "products");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "warehouse_id",
            table: "products",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.CreateIndex(
            name: "ix_products_warehouse_id",
            table: "products",
            column: "warehouse_id");

        migrationBuilder.AddForeignKey(
            name: "fk_products_warehouses_warehouse_id",
            table: "products",
            column: "warehouse_id",
            principalTable: "warehouses",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}
