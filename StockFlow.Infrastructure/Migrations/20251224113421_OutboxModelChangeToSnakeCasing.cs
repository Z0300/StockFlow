using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockFlow.Infrastructure.Migrations;

/// <inheritdoc />
public partial class OutboxModelChangeToSnakeCasing : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameTable(
            name: "OutboxMessages",
            newName: "outbox_messages");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameTable(
            name: "outbox_messages",
            newName: "OutboxMessages");
    }
}
