using System.ComponentModel.DataAnnotations;

namespace Invoice.Domains.Common.Objects
{
    public class ClientAddressObject
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Line1 { get; set; }
        [MaxLength(255)]
        public string? Line2 { get; set; }
        [MaxLength(255)]
        public string? Line3 { get; set; }
        [MaxLength(255)]
        public string? Line4 { get; set; }
        [Required]
        [MaxLength(255)]
        public string City { get; set; }
        [MaxLength(255)]
        public string? Region { get; set; }
        [Required]
        [MaxLength(255)]
        public string PostalCode { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        public string CountryCode { get; set; }
        [Required]
        public bool IsDefault { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public DateTimeOffset? UpdateAt { get; set; }
        public DateTimeOffset? DeleteAt { get; set; }
    }
}
