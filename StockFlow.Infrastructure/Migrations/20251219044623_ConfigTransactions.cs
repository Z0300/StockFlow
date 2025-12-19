using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockFlow.Infrastructure.Migrations;

/// <inheritdoc />
public partial class ConfigTransactions : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "ix_transactions_product_id",
            table: "transactions");

        migrationBuilder.AlterColumn<decimal>(
            name: "unit_cost",
            table: "transactions",
            type: "numeric(18,4)",
            precision: 18,
            scale: 4,
            nullable: true,
            oldClrType: typeof(decimal),
            oldType: "numeric(18,2)",
            oldPrecision: 18,
            oldScale: 2,
            oldNullable: true);

        migrationBuilder.CreateIndex(
            name: "ix_transactions_product_id_warehouse_id",
            table: "transactions",
            columns: ["product_id", "warehouse_id"]);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "ix_transactions_product_id_warehouse_id",
            table: "transactions");

        migrationBuilder.AlterColumn<decimal>(
            name: "unit_cost",
            table: "transactions",
            type: "numeric(18,2)",
            precision: 18,
            scale: 2,
            nullable: true,
            oldClrType: typeof(decimal),
            oldType: "numeric(18,4)",
            oldPrecision: 18,
            oldScale: 4,
            oldNullable: true);

        migrationBuilder.CreateIndex(
            name: "ix_transactions_product_id",
            table: "transactions",
            column: "product_id");
    }
}
