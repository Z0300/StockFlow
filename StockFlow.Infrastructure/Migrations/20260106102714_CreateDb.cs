using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StockFlow.Infrastructure.Migrations;

/// <inheritdoc />
public partial class CreateDb : Migration
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
            name: "outbox_messages",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                occurred_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                type = table.Column<string>(type: "text", nullable: false),
                content = table.Column<string>(type: "json", nullable: false),
                processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                error = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_outbox_messages", x => x.id);
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
                first_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                last_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                email = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                password_hash = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false)
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
                name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
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
                price_amount = table.Column<decimal>(type: "numeric", nullable: false),
                price_currency = table.Column<string>(type: "text", nullable: false),
                category_id = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_products", x => x.id);
                table.ForeignKey(
                    name: "fk_products_categories_category_id",
                    column: x => x.category_id,
                    principalTable: "categories",
                    principalColumn: "id");
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
            name: "refresh_tokens",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                token = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                expires_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_refresh_tokens", x => x.id);
                table.ForeignKey(
                    name: "fk_refresh_tokens_users_user_id",
                    column: x => x.user_id,
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "role_user",
            columns: table => new
            {
                roles_id = table.Column<int>(type: "integer", nullable: false),
                users_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_role_user", x => new { x.roles_id, x.users_id });
                table.ForeignKey(
                    name: "fk_role_user_role_roles_id",
                    column: x => x.roles_id,
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
            name: "transfers",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                source_warehouse_id = table.Column<Guid>(type: "uuid", nullable: false),
                destination_warehouse_id = table.Column<Guid>(type: "uuid", nullable: false),
                status = table.Column<int>(type: "integer", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                dispatch_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                received_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_transfers", x => x.id);
                table.ForeignKey(
                    name: "fk_transfers_warehouses_destination_warehouse_id",
                    column: x => x.destination_warehouse_id,
                    principalTable: "warehouses",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "fk_transfers_warehouses_source_warehouse_id",
                    column: x => x.source_warehouse_id,
                    principalTable: "warehouses",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "order_items",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                order_id = table.Column<Guid>(type: "uuid", nullable: false),
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                quantity = table.Column<int>(type: "integer", nullable: false),
                unit_price_amount = table.Column<decimal>(type: "numeric", nullable: false),
                unit_price_currency = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_order_items", x => x.id);
                table.ForeignKey(
                    name: "fk_order_items_orders_order_id",
                    column: x => x.order_id,
                    principalTable: "orders",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
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
                warehouse_id = table.Column<Guid>(type: "uuid", nullable: false),
                transaction_type = table.Column<int>(type: "integer", nullable: false),
                reason = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                order_id = table.Column<Guid>(type: "uuid", nullable: true),
                transfer_id = table.Column<Guid>(type: "uuid", nullable: true),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_transactions", x => x.id);
                table.ForeignKey(
                    name: "fk_transactions_orders_order_id",
                    column: x => x.order_id,
                    principalTable: "orders",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "fk_transactions_transfers_transfer_id",
                    column: x => x.transfer_id,
                    principalTable: "transfers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "fk_transactions_warehouses_warehouse_id",
                    column: x => x.warehouse_id,
                    principalTable: "warehouses",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "transfer_items",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                transfer_id = table.Column<Guid>(type: "uuid", nullable: false),
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                requested_quantity = table.Column<int>(type: "integer", nullable: false),
                received_quantity = table.Column<int>(type: "integer", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_transfer_items", x => x.id);
                table.ForeignKey(
                    name: "fk_transfer_items_products_product_id",
                    column: x => x.product_id,
                    principalTable: "products",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_transfer_items_transfers_transfer_id",
                    column: x => x.transfer_id,
                    principalTable: "transfers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "transaction_items",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                transaction_id = table.Column<Guid>(type: "uuid", nullable: false),
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                quantity_change = table.Column<int>(type: "integer", nullable: false),
                unit_cost_amount = table.Column<decimal>(type: "numeric", nullable: true),
                unit_cost_currency = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_transaction_items", x => x.id);
                table.ForeignKey(
                    name: "fk_transaction_items_products_product_id",
                    column: x => x.product_id,
                    principalTable: "products",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_transaction_items_transactions_transaction_id",
                    column: x => x.transaction_id,
                    principalTable: "transactions",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "permissions",
            columns: ["id", "name"],
            values: [1, "admin:access"]);

        migrationBuilder.InsertData(
            table: "roles",
            columns: ["id", "name"],
            values: [1, "Admin"]);

        migrationBuilder.InsertData(
            table: "role_permissions",
            columns: ["permission_id", "role_id"],
            values: [1, 1]);

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
            name: "ix_products_name",
            table: "products",
            column: "name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_refresh_tokens_token",
            table: "refresh_tokens",
            column: "token",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_refresh_tokens_user_id",
            table: "refresh_tokens",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_role_permissions_permission_id",
            table: "role_permissions",
            column: "permission_id");

        migrationBuilder.CreateIndex(
            name: "ix_role_user_users_id",
            table: "role_user",
            column: "users_id");

        migrationBuilder.CreateIndex(
            name: "ix_transaction_items_product_id",
            table: "transaction_items",
            column: "product_id");

        migrationBuilder.CreateIndex(
            name: "ix_transaction_items_transaction_id",
            table: "transaction_items",
            column: "transaction_id");

        migrationBuilder.CreateIndex(
            name: "ix_transactions_order_id",
            table: "transactions",
            column: "order_id");

        migrationBuilder.CreateIndex(
            name: "ix_transactions_transfer_id",
            table: "transactions",
            column: "transfer_id");

        migrationBuilder.CreateIndex(
            name: "ix_transactions_warehouse_id",
            table: "transactions",
            column: "warehouse_id");

        migrationBuilder.CreateIndex(
            name: "ix_transfer_items_product_id",
            table: "transfer_items",
            column: "product_id");

        migrationBuilder.CreateIndex(
            name: "ix_transfer_items_transfer_id_product_id",
            table: "transfer_items",
            columns: ["transfer_id", "product_id"],
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_transfers_destination_warehouse_id",
            table: "transfers",
            column: "destination_warehouse_id");

        migrationBuilder.CreateIndex(
            name: "ix_transfers_source_warehouse_id",
            table: "transfers",
            column: "source_warehouse_id");

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
            name: "outbox_messages");

        migrationBuilder.DropTable(
            name: "refresh_tokens");

        migrationBuilder.DropTable(
            name: "role_permissions");

        migrationBuilder.DropTable(
            name: "role_user");

        migrationBuilder.DropTable(
            name: "transaction_items");

        migrationBuilder.DropTable(
            name: "transfer_items");

        migrationBuilder.DropTable(
            name: "permissions");

        migrationBuilder.DropTable(
            name: "roles");

        migrationBuilder.DropTable(
            name: "users");

        migrationBuilder.DropTable(
            name: "transactions");

        migrationBuilder.DropTable(
            name: "products");

        migrationBuilder.DropTable(
            name: "orders");

        migrationBuilder.DropTable(
            name: "transfers");

        migrationBuilder.DropTable(
            name: "categories");

        migrationBuilder.DropTable(
            name: "suppliers");

        migrationBuilder.DropTable(
            name: "warehouses");
    }
}
