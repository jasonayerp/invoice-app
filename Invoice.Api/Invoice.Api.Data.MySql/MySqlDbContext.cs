namespace Invoice.Api.Data.MySql;

public sealed class MySqlDbContext : DbContext
{
    public DbSet<AddressEntity> Addresses { get; set; }

    public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressEntity>(entity =>
        {
            entity.ToTable("addresses");

            entity.Property(e => e.AddressId).HasColumnName("address_id").HasColumnType("int").UseMySqlIdentityColumn();

            entity.HasKey(e => e.AddressId).HasName("pk_addresses");
        });
    }
}
