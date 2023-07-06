using System.ComponentModel.DataAnnotations;

namespace Invoice.Api.Objects;

public class AddressObject
{
    public int Id { get; set; } = 0;
    public Guid PublicId { get; set; } = Guid.Empty;
    [Required]
    [MaxLength(128)]
    public string AddressLine1 { get; set; } = string.Empty;
    [MaxLength(128)]
    public string? AddressLine2 { get; set; }
    [MaxLength(128)]
    public string? AddressLine3 { get; set; }
    [MaxLength(128)]
    public string? AddressLine4 { get; set; }
    [Required]
    [MaxLength(128)]
    public string City { get; set; } = string.Empty;
    [Required]
    [MaxLength(128)]
    public string Region { get; set; } = string.Empty;
    [Required]
    [MaxLength(128)]
    public string PostalCode { get; set; } = string.Empty;
    [Required]
    [MinLength(2)]
    [MaxLength(2)]
    public string CountryCode { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.MinValue;
    public DateTime UtcCreatedDate { get; set; } = DateTime.MinValue;
    public DateTime? UpdatedDate { get; set; }
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }
}
