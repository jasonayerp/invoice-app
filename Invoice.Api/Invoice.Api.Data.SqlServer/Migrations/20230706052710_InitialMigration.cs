using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice.Api.Data.SqlServer.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "dbo",
                columns: table => new
                {
                    AddressId = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    AddressLine1 = table.Column<string>(type: "NVARCHAR(128)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "NVARCHAR(128)", nullable: true, defaultValueSql: "NULL"),
                    AddressLine3 = table.Column<string>(type: "NVARCHAR(128)", nullable: true, defaultValueSql: "NULL"),
                    AddressLine4 = table.Column<string>(type: "NVARCHAR(128)", nullable: true, defaultValueSql: "NULL"),
                    City = table.Column<string>(type: "NVARCHAR(128)", nullable: false),
                    Region = table.Column<string>(type: "NVARCHAR(128)", nullable: false),
                    PostalCode = table.Column<string>(type: "NVARCHAR(128)", nullable: false),
                    CountryCode = table.Column<string>(type: "NCHAR(2)", nullable: false),
                    UtcCreatedDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UtcUpdatedDate = table.Column<DateTime>(type: "DATETIME2", nullable: true, defaultValueSql: "NULL"),
                    UtcDeletedDate = table.Column<DateTime>(type: "DATETIME2", nullable: true, defaultValueSql: "NULL")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "dbo",
                columns: table => new
                {
                    ClientId = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Phone = table.Column<string>(type: "NVARCHAR(50)", nullable: true, defaultValueSql: "NULL"),
                    Email = table.Column<string>(type: "NVARCHAR(50)", nullable: true, defaultValueSql: "NULL"),
                    UtcCreatedDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UtcUpdatedDate = table.Column<DateTime>(type: "DATETIME2", nullable: true, defaultValueSql: "NULL"),
                    UtcDeletedDate = table.Column<DateTime>(type: "DATETIME2", nullable: true, defaultValueSql: "NULL")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                schema: "dbo",
                columns: table => new
                {
                    InvoiceId = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    Number = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UtcDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    Status = table.Column<short>(type: "SMALLINT", nullable: false),
                    PaymentTerm = table.Column<short>(type: "SMALLINT", nullable: false),
                    BillFromAddressId = table.Column<long>(type: "BIGINT", nullable: false),
                    BillToAddressId = table.Column<long>(type: "BIGINT", nullable: false),
                    ClientId = table.Column<long>(type: "BIGINT", nullable: false),
                    UtcCreatedDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UtcUpdatedDate = table.Column<DateTime>(type: "DATETIME2", nullable: true, defaultValueSql: "NULL"),
                    UtcDeletedDate = table.Column<DateTime>(type: "DATETIME2", nullable: true, defaultValueSql: "NULL")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Invoices_Addresses_BillFromAddressId",
                        column: x => x.BillFromAddressId,
                        principalSchema: "dbo",
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                    table.ForeignKey(
                        name: "FK_Invoices_Addresses_BillToAddressId",
                        column: x => x.BillToAddressId,
                        principalSchema: "dbo",
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                    table.ForeignKey(
                        name: "FK_Invoices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "dbo",
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                schema: "dbo",
                columns: table => new
                {
                    InvoiceItemId = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<long>(type: "BIGINT", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(128)", nullable: false),
                    Quantity = table.Column<int>(type: "INT", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(19,4)", precision: 19, scale: 4, nullable: false),
                    UtcCreatedDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UtcUpdatedDate = table.Column<DateTime>(type: "DATETIME2", nullable: true, defaultValueSql: "NULL"),
                    UtcDeletedDate = table.Column<DateTime>(type: "DATETIME2", nullable: true, defaultValueSql: "NULL")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItem", x => x.InvoiceItemId)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "dbo",
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_City",
                schema: "dbo",
                table: "Addresses",
                column: "City")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryCode",
                schema: "dbo",
                table: "Addresses",
                column: "CountryCode")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Region",
                schema: "dbo",
                table: "Addresses",
                column: "Region")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "UX_Addresses_Address",
                schema: "dbo",
                table: "Addresses",
                columns: new[] { "AddressLine1", "City", "Region", "PostalCode", "CountryCode" },
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Client_Guid",
                schema: "dbo",
                table: "Clients",
                column: "Guid")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "UX_Client_Name",
                schema: "dbo",
                table: "Clients",
                column: "Name",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                schema: "dbo",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "UX_InvoiceItems_Description",
                schema: "dbo",
                table: "InvoiceItems",
                column: "Description",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BillFromAddressId",
                schema: "dbo",
                table: "Invoices",
                column: "BillFromAddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BillToAddressId",
                schema: "dbo",
                table: "Invoices",
                column: "BillToAddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ClientId",
                schema: "dbo",
                table: "Invoices",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "UX_Invoices_Number",
                schema: "dbo",
                table: "Invoices",
                column: "Number",
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItems",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Invoices",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "dbo");
        }
    }
}
