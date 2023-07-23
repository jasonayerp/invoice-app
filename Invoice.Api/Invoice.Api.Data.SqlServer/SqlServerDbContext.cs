namespace Invoice.Api.Data.SqlServer;

public class SqlServerDbContext : DbContext
{
    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<ClientAddressEntity> ClientAddresses { get; set; }
    public DbSet<InvoiceEntity> Invoices { get; set; }
    public DbSet<InvoiceItemEntity> InvoicesItems { get; set; }

    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.HasDefaultSchema("dbo")
            .ApplyConfigurationsFromAssembly(typeof(SqlServerDbContext).Assembly);
    }
}
