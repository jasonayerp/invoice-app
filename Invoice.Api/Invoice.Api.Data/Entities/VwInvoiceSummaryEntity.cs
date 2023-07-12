namespace Invoice.Api.Data.Entities;

public class VwInvoiceSummaryEntity
{
    public int InvoiceId { get; private set; }
    public Guid Guid { get; private set; }
    public string Number { get; private set; }
    public DateTime UtcDate { get; private set; }
    public int Status { get; private set; }
    public DateTime UtcCreatedDate { get; private set; }
    public DateTime? UtcUpdatedDate { get; private set; }
    public DateTime? UtcDeletedDate { get; private set; }
    public decimal InvoiceItemsTotalAmount { get; private set; }
    public int InvocieItemsCount { get; private set; }
    public int BillFromAddressId { get; private set; }
    public string BillFromAddressLine1 { get; private set; }
    public string BillFromAddressLine2 { get; private set; }
    public string BillFromAddressLine3 { get; private set; }
    public string BillFromAddressLine4 { get; private set; }
    public string BillFromCity { get; private set; }
    public string BillFromRegion { get; private set; }
    public string BillFromPostalCode { get; private set; }
    public string BillFromCountryCode { get; private set; }
    public int BillToAddressId { get; private set; }
    public string BillToAddressLine1 { get; private set; }
    public string BillToAddressLine2 { get; private set; }
    public string BillToAddressLine3 { get; private set; }
    public string BillToAddressLine4 { get; private set; }
    public string BillToCity { get; private set; }
    public string BillToRegion { get; private set; }
    public string BillToPostalCode { get; private set; }
    public string BillToCountryCode { get; private set; }
    public int ClientId { get; private set; }
    public string ClientName { get; private set; }
    public string ClientPhone { get; private set; }
    public string ClientEmail { get; private set; }
}
