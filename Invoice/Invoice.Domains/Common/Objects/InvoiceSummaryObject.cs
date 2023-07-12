namespace Invoice.Domains.Common.Objects;

public class InvoiceSummaryObject
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Number { get; set; }
    public DateTime UtcDate { get; set; }
    public int Status { get; set; }
    public DateTime UtcCreatedDate { get; set; }
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }
    public decimal InvoiceItemsTotalAmount { get; set; }
    public int InvocieItemsCount { get; set; }
    public int BillFromAddressId { get; set; }
    public string BillFromAddressLine1 { get; set; }
    public string BillFromAddressLine2 { get; set; }
    public string BillFromAddressLine3 { get; set; }
    public string BillFromAddressLine4 { get; set; }
    public string BillFromCity { get; set; }
    public string BillFromRegion { get; set; }
    public string BillFromPostalCode { get; set; }
    public string BillFromCountryCode { get; set; }
    public int BillToAddressId { get; set; }
    public string BillToAddressLine1 { get; set; }
    public string BillToAddressLine2 { get; set; }
    public string BillToAddressLine3 { get; set; }
    public string BillToAddressLine4 { get; set; }
    public string BillToCity { get; set; }
    public string BillToRegion { get; set; }
    public string BillToPostalCode { get; set; }
    public string BillToCountryCode { get; set; }
    public int ClientId { get; set; }
    public string ClientName { get; set; }
    public string ClientPhone { get; set; }
    public string ClientEmail { get; set; }
}
