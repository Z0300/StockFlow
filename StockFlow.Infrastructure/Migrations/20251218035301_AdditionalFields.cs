using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockFlow.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AdditionalFields : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "timestamp",
            table: "inventory_transactions",
            newName: "created_at");

        migrationBuilder.AddColumn<string>(
            name: "reason",
            table: "inventory_transactions",
            type: "character varying(512)",
            maxLength: 512,
            nullable: true);

        migrationBuilder.AddColumn<decimal>(
            name: "unit_cost",
            table: "inventory_transactions",
            type: "numeric(18,2)",
            precision: 18,
            scale: 2,
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "warehouse_id",
            table: "inventory_transactions",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.CreateIndex(
            name: "ix_inventory_transactions_warehouse_id",
            table: "inventory_transactions",
            column: "warehouse_id");

        migrationBuilder.AddForeignKey(
            name: "fk_inventory_transactions_warehouses_warehouse_id",
            table: "inventory_transactions",
            column: "warehouse_id",
            principalTable: "warehouses",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_inventory_transactions_warehouses_warehouse_id",
            table: "inventory_transactions");

        migrationBuilder.DropIndex(
            name: "ix_inventory_transactions_warehouse_id",
            table: "inventory_transactions");

        migrationBuilder.DropColumn(
            name: "reason",
            table: "inventory_transactions");

        migrationBuilder.DropColumn(
            name: "unit_cost",
            table: "inventory_transactions");

        migrationBuilder.DropColumn(
            name: "warehouse_id",
            table: "inventory_transactions");

        migrationBuilder.RenameColumn(
            name: "created_at",
            table: "inventory_transactions",
            newName: "timestamp");
    }
}
