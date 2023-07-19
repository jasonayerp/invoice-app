using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice.Api.Data.SqlServer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "dbo",
                columns: table => new
                {
                    ClientId = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "(NEWID())"),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(56)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(128)", nullable: true, defaultValueSql: "(NULL)"),
                    UtcCreatedDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UtcUpdatedDate = table.Column<DateTime>(type: "DATETIME2", nullable: true, defaultValueSql: "(NULL)"),
                    UtcDeletedDate = table.Column<DateTime>(type: "DATETIME2", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "ClientAddresses",
                schema: "dbo",
                columns: table => new
                {
                    ClientAddressId = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<long>(type: "BIGINT", nullable: false),
                    AddressLine1 = table.Column<string>(type: "NVARCHAR(128)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "NVARCHAR(128)", nullable: true, defaultValueSql: "(NULL)"),
                    AddressLine3 = table.Column<string>(type: "NVARCHAR(128)", nullable: true, defaultValueSql: "(NULL)"),
                    AddressLine4 = table.Column<string>(type: "NVARCHAR(128)", nullable: true, defaultValueSql: "(NULL)"),
                    City = table.Column<string>(type: "NVARCHAR(96)", nullable: false),
                    Region = table.Column<string>(type: "NVARCHAR(96)", nullable: true, defaultValueSql: "(NULL)"),
                    PostalCode = table.Column<string>(type: "NVARCHAR(8)", nullable: false),
                    CountryCode = table.Column<string>(type: "NCHAR(2)", nullable: false),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false),
                    IsPrimary = table.Column<bool>(type: "BIT", nullable: false),
                    UtcCreatedDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UtcUpdatedDate = table.Column<DateTime>(type: "DATETIME2", nullable: true, defaultValueSql: "(NULL)"),
                    UtcDeletedDate = table.Column<DateTime>(type: "DATETIME2", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.ClientAddressId)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_ClientAddresses_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "dbo",
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                schema: "dbo",
                columns: table => new
                {
                    InvoiceId = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "(NEWID())"),
                    ClientId = table.Column<long>(type: "BIGINT", nullable: false),
                    Number = table.Column<string>(type: "NVARCHAR(8)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(128)", nullable: false),
                    UtcDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    UtcDueDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    Status = table.Column<short>(type: "SMALLINT", nullable: false),
                    PaymentTermDays = table.Column<short>(type: "SMALLINT", nullable: false),
                    UtcCreatedDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UtcUpdatedDate = table.Column<DateTime>(type: "DATETIME2", nullable: true, defaultValueSql: "(NULL)"),
                    UtcDeletedDate = table.Column<DateTime>(type: "DATETIME2", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId)
                        .Annotation("SqlServer:Clustered", true);
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
                    UtcUpdatedDate = table.Column<DateTime>(type: "DATETIME2", nullable: true, defaultValueSql: "(NULL)"),
                    UtcDeletedDate = table.Column<DateTime>(type: "DATETIME2", nullable: true, defaultValueSql: "(NULL)")
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
                table: "ClientAddresses",
                column: "City")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryCode",
                schema: "dbo",
                table: "ClientAddresses",
                column: "CountryCode")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Region",
                schema: "dbo",
                table: "ClientAddresses",
                column: "Region")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "UX_ClientAddresses_Address",
                schema: "dbo",
                table: "ClientAddresses",
                columns: new[] { "ClientId", "AddressLine1", "AddressLine2", "AddressLine3", "AddressLine4", "City", "Region", "PostalCode", "CountryCode" },
                unique: true,
                filter: "[AddressLine2] IS NOT NULL AND [AddressLine3] IS NOT NULL AND [AddressLine4] IS NOT NULL AND [Region] IS NOT NULL")
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
                name: "IX_Invoices_UtcDate",
                schema: "dbo",
                table: "Invoices",
                column: "UtcDate")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UtcDueDate",
                schema: "dbo",
                table: "Invoices",
                column: "UtcDueDate")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "UX_Invoices_ClientId_Number",
                schema: "dbo",
                table: "Invoices",
                columns: new[] { "ClientId", "Number" },
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientAddresses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InvoiceItems",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Invoices",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "dbo");
        }
    }
}
