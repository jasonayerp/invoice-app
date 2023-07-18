using Invoice.Api.Data.Documents;

namespace Invoice.Api.Data.Cosmos;

public sealed class CosmosDbContext : DbContext
{
    public DbSet<ClientAppSettingDocument> ClientAppSettings { get; set; }

    public CosmosDbContext(DbContextOptions<CosmosDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientAppSettingDocument>(entity =>
        {
            entity.ToContainer("ClientAppSettings");

            entity.Property(e => e.Id).ToJsonProperty("Id");
            entity.Property(e => e.Key).ToJsonProperty("Key");
            entity.Property(e => e.Value).ToJsonProperty("Value");

            entity.HasPartitionKey(e => e.Id);
            entity.HasKey(e => e.Key);
        });
    }
}
