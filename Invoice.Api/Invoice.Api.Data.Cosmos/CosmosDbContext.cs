using Invoice.Api.Data.Documents;

namespace Invoice.Api.Data.Cosmos;

public sealed class CosmosDbContext : DbContext
{
    public DbSet<OrganizationDocument> Organizations { get; set; }

    public CosmosDbContext(DbContextOptions<CosmosDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrganizationDocument>(entity =>
        {
            entity.ToContainer("organizations");

            entity.Property(e => e.Id).ToJsonProperty("_id");
            entity.Property(e => e.Guid).ToJsonProperty("guid");
            entity.Property(e => e.Name).ToJsonProperty("name");
            entity.Property(e => e.AddressLine1).ToJsonProperty("address_line_1");
            entity.Property(e => e.AddressLine2).ToJsonProperty("address_line_2");
            entity.Property(e => e.AddressLine3).ToJsonProperty("address_line_3");
            entity.Property(e => e.AddressLine4).ToJsonProperty("address_line_4");
            entity.Property(e => e.City).ToJsonProperty("city");
            entity.Property(e => e.Region).ToJsonProperty("region");
            entity.Property(e => e.PostalCode).ToJsonProperty("postal_code");
            entity.Property(e => e.CountryCode).ToJsonProperty("country_code");
            entity.Property(e => e.DefaultPaymentTerm).ToJsonProperty("default_payment_term");
            entity.Property(e => e.UtcCreatedDate).ToJsonProperty("utc_created_date");
            entity.Property(e => e.UtcUpdatedDate).ToJsonProperty("utc_updated_date");
            entity.Property(e => e.UtcDeletedDate).ToJsonProperty("utc_deleted_date");

            entity.HasData(new OrganizationDocument
            {
                Name = "FrontendMentor.io",
                AddressLine1 = "19 Union Terrace",
                City = "London",
                PostalCode = "E1 3EZ",
                CountryCode = "UK",
                DefaultPaymentTerm = 4
            });
        });
    }
}
