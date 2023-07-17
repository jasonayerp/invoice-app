﻿namespace Invoice.Api.Data.Entities;

public class AddressEntity
{
    public int AddressId { get; set; }
    public Guid Guid { get; set; }
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressLine4 { get; set; }
    public string City { get; set; }
    public string? Region { get; set; }
    public string PostalCode { get; set; }
    public string CountryCode { get; set; }
    public DateTime UtcCreatedDate { get; set; }
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }
}
