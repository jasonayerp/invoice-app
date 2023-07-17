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
            entity.ToContainer("Organizations");

            entity.Property(e => e.Id).ToJsonProperty("organizationId");
            entity.Property(e => e.Guid).ToJsonProperty("guid");
            entity.Property(e => e.Name).ToJsonProperty("name");
            entity.Property(e => e.AddressLine1).ToJsonProperty("AddressLine1");
            entity.Property(e => e.AddressLine2).ToJsonProperty("AddressLine2");
            entity.Property(e => e.AddressLine3).ToJsonProperty("AddressLine3");
            entity.Property(e => e.AddressLine4).ToJsonProperty("AddressLine4");
            entity.Property(e => e.City).ToJsonProperty("City");
            entity.Property(e => e.Region).ToJsonProperty("Region");
            entity.Property(e => e.PostalCode).ToJsonProperty("PostalCode");
            entity.Property(e => e.CountryCode).ToJsonProperty("CountryCode");
            entity.Property(e => e.UtcCreatedDate).ToJsonProperty("utcCreatedDate");
            entity.Property(e => e.UtcUpdatedDate).ToJsonProperty("utcDeletedDate");
            entity.Property(e => e.Attributes).ToJsonProperty("attributes");
        });
    }
}
