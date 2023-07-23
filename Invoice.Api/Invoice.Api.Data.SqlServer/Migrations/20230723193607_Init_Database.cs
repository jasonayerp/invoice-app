using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice.Api.Data.SqlServer.Migrations
{
    public partial class Init_Database : Migration
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
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "NVARCHAR(56)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(128)", nullable: true, defaultValueSql: "NULL"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET(7)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET(7)", nullable: true, defaultValueSql: "NULL"),
                    DeletedAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET(7)", nullable: true, defaultValueSql: "NULL")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientAddresses",
                schema: "dbo",
                columns: table => new
                {
                    ClientAddressId = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<long>(type: "BIGINT", nullable: false),
                    Line1 = table.Column<string>(type: "NVARCHAR(128)", nullable: false),
                    Line2 = table.Column<string>(type: "NVARCHAR(128)", nullable: true, defaultValueSql: "NULL"),
                    Line3 = table.Column<string>(type: "NVARCHAR(128)", nullable: true, defaultValueSql: "NULL"),
                    Line4 = table.Column<string>(type: "NVARCHAR(128)", nullable: true, defaultValueSql: "NULL"),
                    City = table.Column<string>(type: "NVARCHAR(96)", nullable: false),
                    Region = table.Column<string>(type: "NVARCHAR(96)", nullable: true, defaultValueSql: "NULL"),
                    PostalCode = table.Column<string>(type: "NVARCHAR(8)", nullable: false),
                    CountryCode = table.Column<string>(type: "NCHAR(2)", nullable: false),
                    IsDefault = table.Column<bool>(type: "BIT", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET(7)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET(7)", nullable: true, defaultValueSql: "NULL"),
                    DeletedAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET(7)", nullable: true, defaultValueSql: "NULL")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.ClientAddressId);
                    table.ForeignKey(
                        name: "FK_ClientAddresses_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "dbo",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                schema: "dbo",
                columns: table => new
                {
                    InvoiceId = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    ClientId = table.Column<long>(type: "BIGINT", nullable: false),
                    Number = table.Column<string>(type: "NVARCHAR(8)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(128)", nullable: false),
                    Date = table.Column<DateTime>(type: "DATE", nullable: false),
                    DueDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    Status = table.Column<short>(type: "SMALLINT(1)", nullable: false),
                    PaymentTermDays = table.Column<short>(type: "SMALLINT(2)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET(7)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET(7)", nullable: true, defaultValueSql: "NULL"),
                    DeletedAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET(7)", nullable: true, defaultValueSql: "NULL")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "dbo",
                        principalTable: "Clients",
                        principalColumn: "Id",
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
                    UnitPrice = table.Column<decimal>(type: "DECIMAL(19,4)", precision: 19, scale: 4, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, computedColumnSql: "([Quantity] * [UnitPrice])"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET(7)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET(7)", nullable: true, defaultValueSql: "NULL"),
                    DeletedAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET(7)", nullable: true, defaultValueSql: "NULL")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.InvoiceItemId);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "dbo",
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UX_ClientAddresses_Address",
                schema: "dbo",
                table: "ClientAddresses",
                columns: new[] { "ClientId", "Line1", "Line2", "Line3", "Line4", "City", "Region", "PostalCode", "CountryCode" },
                unique: true,
                filter: "[Line2] IS NOT NULL AND [Line3] IS NOT NULL AND [Line4] IS NOT NULL AND [Region] IS NOT NULL")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "UX_ClientAddresses_DefaultAddress",
                schema: "dbo",
                table: "ClientAddresses",
                columns: new[] { "ClientId", "IsDefault" },
                unique: true)
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
                name: "IX_Invoices_Date",
                schema: "dbo",
                table: "Invoices",
                column: "Date")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_DueDate",
                schema: "dbo",
                table: "Invoices",
                column: "DueDate")
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
