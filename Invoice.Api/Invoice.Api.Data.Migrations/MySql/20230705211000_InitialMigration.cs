using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice.Api.Data.Migrations.MySql
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    address_id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    guid = table.Column<string>(type: "VARCHAR(32)", nullable: false, defaultValueSql: "(UUID())")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address_line_1 = table.Column<string>(type: "VARCHAR(128)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address_line_2 = table.Column<string>(type: "VARCHAR(128)", nullable: true, defaultValueSql: "null")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address_line_3 = table.Column<string>(type: "VARCHAR(128)", nullable: true, defaultValueSql: "null")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address_line_4 = table.Column<string>(type: "VARCHAR(128)", nullable: true, defaultValueSql: "null")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    city = table.Column<string>(type: "VARCHAR(128)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    region = table.Column<string>(type: "VARCHAR(128)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    postal_code = table.Column<string>(type: "VARCHAR(128)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country_code = table.Column<string>(type: "CHAR(2)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    utc_created_date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    utc_updated_date = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "null"),
                    utc_deleted_date = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "null")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.address_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    client_id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    guid = table.Column<string>(type: "VARCHAR(32)", nullable: false, defaultValueSql: "(UUID())")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "VARCHAR(50)", nullable: true, defaultValueSql: "null")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "VARCHAR(50)", nullable: true, defaultValueSql: "null")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    utc_created_date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    utc_updated_date = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "null"),
                    utc_deleted_date = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "null")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.client_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    invoice_id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    guid = table.Column<string>(type: "VARCHAR(32)", nullable: false, defaultValueSql: "(UUID())")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    number = table.Column<string>(type: "VARCHAR(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    utc_date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    status = table.Column<short>(type: "SMALLINT", nullable: false),
                    payment_term = table.Column<short>(type: "SMALLINT", nullable: false),
                    bill_from_address_id = table.Column<long>(type: "BIGINT", nullable: false),
                    bill_to_address_id = table.Column<long>(type: "BIGINT", nullable: false),
                    client_id = table.Column<long>(type: "BIGINT", nullable: false),
                    utc_created_date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    utc_updated_date = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "null"),
                    utc_deleted_date = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "null")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_invoices", x => x.invoice_id);
                    table.ForeignKey(
                        name: "fk_invoices_addresses_bill_from_address_id",
                        column: x => x.bill_from_address_id,
                        principalTable: "addresses",
                        principalColumn: "address_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_invoices_addresses_bill_ftom_address_id",
                        column: x => x.bill_to_address_id,
                        principalTable: "addresses",
                        principalColumn: "address_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_invoices_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "client_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "invoice_items",
                columns: table => new
                {
                    invoice_item_id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    invoice_id = table.Column<long>(type: "BIGINT", nullable: false),
                    description = table.Column<string>(type: "VARCHAR(128)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quantity = table.Column<short>(type: "SMALLINT", nullable: false),
                    amount = table.Column<decimal>(type: "DECIMAL(19,4)", precision: 19, scale: 4, nullable: false),
                    utc_created_date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    utc_updated_date = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "null"),
                    utc_deleted_date = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "null")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_invoice_item", x => x.invoice_item_id);
                    table.ForeignKey(
                        name: "fk_invoice_item_invoice_invoice_id",
                        column: x => x.invoice_id,
                        principalTable: "invoices",
                        principalColumn: "invoice_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_addresses_country_code",
                table: "addresses",
                column: "country_code");

            migrationBuilder.CreateIndex(
                name: "ix_addresses_cty",
                table: "addresses",
                column: "city");

            migrationBuilder.CreateIndex(
                name: "ix_addresses_region",
                table: "addresses",
                column: "region");

            migrationBuilder.CreateIndex(
                name: "ux_addresses_address",
                table: "addresses",
                columns: new[] { "address_line_1", "city", "region", "postal_code", "country_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Iix_client_guid",
                table: "clients",
                column: "guid");

            migrationBuilder.CreateIndex(
                name: "ux_client_name",
                table: "clients",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_invoice_items_invoice_id",
                table: "invoice_items",
                column: "invoice_id");

            migrationBuilder.CreateIndex(
                name: "ux_invoice_items_description",
                table: "invoice_items",
                column: "description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_invoices_bill_from_address_id",
                table: "invoices",
                column: "bill_from_address_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_invoices_bill_to_address_id",
                table: "invoices",
                column: "bill_to_address_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_invoices_client_id",
                table: "invoices",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ux_invoices_number",
                table: "invoices",
                column: "number",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "invoice_items");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "clients");
        }
    }
}
