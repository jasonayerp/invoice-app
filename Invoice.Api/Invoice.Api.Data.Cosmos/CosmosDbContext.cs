namespace Invoice.Api.Data.Cosmos;

public sealed class CosmosDbContext : DbContext
{
    public DbSet<AppClientDocument> ClientAppSettings { get; set; }

    public CosmosDbContext(DbContextOptions<CosmosDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppClientDocument>(entity =>
        {
            entity.ToContainer("ClientAppSettings");

            entity.Property(e => e.Id).ToJsonProperty("id");
            entity.Property(e => e.ClientId).ToJsonProperty("clientId");
            entity.Property(e => e.Settings).ToJsonProperty("settings");

            entity.HasPartitionKey(e => e.Id);
            entity.HasKey(e => e.ClientId);
        });
    }
}
