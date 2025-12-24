using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockFlow.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddDispatchAtAndReceiveAtProperty : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "dispatch_at",
            table: "transfers",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "received_at",
            table: "transfers",
            type: "timestamp with time zone",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "dispatch_at",
            table: "transfers");

        migrationBuilder.DropColumn(
            name: "received_at",
            table: "transfers");
    }
}
