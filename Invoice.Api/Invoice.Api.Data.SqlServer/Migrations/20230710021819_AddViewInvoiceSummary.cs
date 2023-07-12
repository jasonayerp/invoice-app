using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice.Api.Data.SqlServer.Migrations
{
    public partial class AddViewInvoiceSummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW [vw_InvoiceSummary] AS
SELECT
		[I].[InvoiceId]
	,	[I].[Guid]
	,	[I].[Number]
	,	[I].[UtcDate]
	,	[I].[Status]
	,	[I].[PaymentTerm]
	,	[I].[UtcCreatedDate]
	,	[I].[UtcUpdatedDate]
	,	[I].[UtcDeletedDate]
	,	SUM([II].[Amount]) AS [InvoiceItemsTotalAmount]
	,	COUNT([II].[InvoiceId]) AS [InvoiceItemsCount]
	,	[A].[AddressId] AS [BillFromAddressId]
	,	[A].[AddressLine1] AS [BillFromAddressLine1]
	,	[A].[AddressLine2] AS [BillFromAddressLine2]
	,	[A].[AddressLine3] AS [BillFromAddressLine3]
	,	[A].[AddressLine4] AS [BillFromAddressLin4]
	,	[A].[City] AS [BillFromCity]
	,	[A].[Region] AS [BillFromRegion]
	,	[A].[PostalCode] AS [BillFromPostalCode]
	,	[A].[CountryCode] AS [BillFromCountryCode]
	,	[A1].[AddressId] AS [BillToAddressId]
	,	[A1].[AddressLine1] AS [BillToAddressLine1]
	,	[A1].[AddressLine2] AS [BillToAddressLine2]
	,	[A1].[AddressLine3] AS [BillToAddressLine3]
	,	[A1].[AddressLine4] AS [BillToAddressLine4]
	,	[A1].[City] AS [BillToAddressCity]
	,	[A1].[Region] AS [BillToRegion]
	,	[A1].[PostalCode] AS [BillToPostalCode]
	,	[A1].[CountryCode] AS [BillToCountryCode]
	,	[C].[ClientId]
	,	[C].[ClientId]
    ,	[C].[Name] AS [ClientName]
    ,	[C].[Phone] AS [ClientPhone]
    ,	[C].[Email] AS [ClientEmail]
FROM [dbo].[Invoices] AS [I]
LEFT JOIN [dbo].[InvoiceItems] AS [II]
	ON	[I].[InvoiceId] = [II].[InvoiceId]
INNER JOIN [dbo].[Addresses] AS [A]
	ON	[I].[BillFromAddressId] = [A].[AddressId]
INNER JOIN [dbo].[Addresses] AS [A1]
	ON	[I].[BillToAddressId] = [A1].[AddressId]
INNER JOIN [dbo].[Clients] AS [C]
	ON	[I].[ClientId] = [C].[ClientId]
GROUP BY
		[I].[InvoiceId]
	,	[I].[Guid]
	,	[I].[Number]
	,	[I].[UtcDate]
	,	[I].[Status]
	,	[I].[PaymentTerm]
	,	[I].[BillFromAddressId]
	,	[I].[BillToAddressId]
	,	[I].[ClientId]
	,	[I].[UtcCreatedDate]
	,	[I].[UtcUpdatedDate]
	,	[I].[UtcDeletedDate]
	,	[A].[AddressId]
	,	[A].[AddressLine1]
	,	[A].[AddressLine2]
	,	[A].[AddressLine3]
	,	[A].[AddressLine4]
	,	[A].[City]
	,	[A].[Region]
	,	[A].[PostalCode]
	,	[A].[CountryCode]
	,	[A1].[AddressId]
	,	[A1].[AddressLine1]
	,	[A1].[AddressLine2]
	,	[A1].[AddressLine3]
	,	[A1].[AddressLine4]
	,	[A1].[City]
	,	[A1].[Region]
	,	[A1].[PostalCode]
	,	[A1].[CountryCode]
	,	[C].[ClientId]
	,	[C].[Name]
	,	[C].[Phone]
	,	[C].[Email];");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"DROP VIEW [InvoiceSummaryView];");
        }
    }
}
