using System.ComponentModel.DataAnnotations;

namespace Invoice.Domains.Common.Objects
{
    public class ClientAddressObject
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [Required]
        [MaxLength(255)]
        public string AddressLine1 { get; set; } = "";
        [MaxLength(255)]
        public string? AddressLine2 { get; set; }
        [MaxLength(255)]
        public string? AddressLine3 { get; set; }
        [MaxLength(255)]
        public string? AddressLine4 { get; set; }
        [Required]
        [MaxLength(255)]
        public string City { get; set; }
        [MaxLength(255)]
        public string? Region { get; set; }
        [Required]
        [MaxLength(255)]
        public string PostalCode { get; set; } = "";
        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        public string CountryCode { get; set; } = "";
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsPrimary { get; set; }
        public DateTime UtcCreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UtcUpdatedDate { get; set; }
        public DateTime? UtcDeletedDate { get; set; }
    }
}
