using Invoice.Api.Data.Entities;

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
        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.Entity<ClientEntity>(entity =>
        {
            entity.ToTable("Clients");

            entity.Property(e => e.ClientId).HasColumnName("ClientId").HasColumnType("BIGINT").UseIdentityColumn();
            entity.Property(e => e.Guid).HasColumnName("Guid").HasColumnType("UNIQUEIDENTIFIER").IsRequired().HasDefaultValueSql("(NEWID())").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("Name").HasColumnType("NVARCHAR(56)").IsRequired();
            entity.Property(e => e.Email).HasColumnName("Email").HasColumnType("NVARCHAR(128)").IsRequired(false).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UtcCreatedDate).HasColumnName("UtcCreatedDate").HasColumnType("DATETIME2").IsRequired();
            entity.Property(e => e.UtcUpdatedDate).HasColumnName("UtcUpdatedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UtcDeletedDate).HasColumnName("UtcDeletedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("(NULL)");

            entity.HasKey(e => e.ClientId).IsClustered().HasName("PK_Clients");
            entity.HasIndex(e => e.Guid).IsClustered(false).HasDatabaseName("IX_Client_Guid");
            entity.HasIndex(e => e.Name).IsClustered(false).IsUnique().HasDatabaseName("UX_Client_Name");

            entity.HasQueryFilter(e => e.UtcDeletedDate == null);
        });

        modelBuilder.Entity<ClientAddressEntity>(entity =>
        {
            entity.ToTable("ClientAddresses");

            entity.Property(e => e.ClientAddressId).HasColumnName("ClientAddressId").HasColumnType("BIGINT").UseIdentityColumn();
            entity.Property(e => e.ClientId).HasColumnName("ClientId").HasColumnType("BIGINTT").IsRequired();
            entity.Property(e => e.AddressLine1).HasColumnName("AddressLine1").HasColumnType("NVARCHAR(128)").IsRequired();
            entity.Property(e => e.AddressLine2).HasColumnName("AddressLine2").HasColumnType("NVARCHAR(128)").IsRequired(false).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.AddressLine3).HasColumnName("AddressLine3").HasColumnType("NVARCHAR(128)").IsRequired(false).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.AddressLine4).HasColumnName("AddressLine4").HasColumnType("NVARCHAR(128)").IsRequired(false).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.City).HasColumnName("City").HasColumnType("NVARCHAR(96)").IsRequired();
            entity.Property(e => e.Region).HasColumnName("Region").HasColumnType("NVARCHAR(96)").IsRequired(false).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.PostalCode).HasColumnName("PostalCode").HasColumnType("NVARCHAR(8)").IsRequired();
            entity.Property(e => e.CountryCode).HasColumnName("CountryCode").HasColumnType("NCHAR(2)").IsRequired();
            entity.Property(e => e.IsActive).HasColumnName("IsActive").HasColumnType("BIT").IsRequired();
            entity.Property(e => e.IsPrimary).HasColumnName("IsPrimary").HasColumnType("BIT").IsRequired();
            entity.Property(e => e.UtcCreatedDate).HasColumnName("UtcCreatedDate").HasColumnType("DATETIME2").IsRequired();
            entity.Property(e => e.UtcUpdatedDate).HasColumnName("UtcUpdatedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UtcDeletedDate).HasColumnName("UtcDeletedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("(NULL)");

            entity.HasKey(e => e.ClientAddressId).IsClustered().HasName("PK_Addresses");
            entity.HasIndex(e => e.City).IsClustered(false).HasDatabaseName("IX_Addresses_City");
            entity.HasIndex(e => e.Region).IsClustered(false).HasDatabaseName("IX_Addresses_Region");
            entity.HasIndex(e => e.CountryCode).IsClustered(false).HasDatabaseName("IX_Addresses_CountryCode");
            entity.HasIndex(e => new { e.ClientId, e.AddressLine1, e.AddressLine2, e.AddressLine3, e.AddressLine4, e.City, e.Region, e.PostalCode, e.CountryCode }).IsClustered(false).IsUnique().HasDatabaseName("UX_ClientAddresses_Address");

            entity.HasOne(e => e.Client).WithMany(e => e.Addresses).HasForeignKey(e => e.ClientId).HasConstraintName("FK_ClientAddresses_Clients_ClientId").OnDelete(DeleteBehavior.Cascade);

            entity.HasQueryFilter(e => e.UtcDeletedDate == null);
        });

        modelBuilder.Entity<InvoiceEntity>(entity =>
        {
            entity.ToTable("Invoices");

            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceId").HasColumnType("BIGINT").UseIdentityColumn();
            entity.Property(e => e.Guid).HasColumnName("Guid").HasColumnType("UNIQUEIDENTIFIER").IsRequired().HasDefaultValueSql("(NEWID())").ValueGeneratedOnAdd();
            entity.Property(e => e.ClientId).HasColumnName("ClientId").HasColumnType("BIGINT").IsRequired();
            entity.Property(e => e.Number).HasColumnName("Number").HasColumnType("NVARCHAR(8)").IsRequired();
            entity.Property(e => e.Description).HasColumnName("Description").HasColumnType("NVARCHAR(128)").IsRequired();
            entity.Property(e => e.UtcDate).HasColumnName("UtcDate").HasColumnType("DATE").IsRequired();
            entity.Property(e => e.UtcDueDate).HasColumnName("UtcDueDate").HasColumnType("DATE").IsRequired();
            entity.Property(e => e.Status).HasColumnName("Status").HasColumnType("SMALLINT").IsRequired();
            entity.Property(e => e.PaymentTermDays).HasColumnName("PaymentTermDays").HasColumnType("SMALLINT").IsRequired();
            entity.Property(e => e.UtcCreatedDate).HasColumnName("UtcCreatedDate").HasColumnType("DATETIME2").IsRequired();
            entity.Property(e => e.UtcUpdatedDate).HasColumnName("UtcUpdatedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UtcDeletedDate).HasColumnName("UtcDeletedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("(NULL)");

            entity.HasKey(e => e.InvoiceId).IsClustered().HasName("PK_Invoices");
            entity.HasIndex(e => new { e.ClientId, e.Number }).IsClustered(false).IsUnique().HasDatabaseName("UX_Invoices_ClientId_Number");
            entity.HasIndex(e => e.UtcDate).IsClustered(false).HasDatabaseName("IX_Invoices_UtcDate");
            entity.HasIndex(e => e.UtcDueDate).IsClustered(false).HasDatabaseName("IX_Invoices_UtcDueDate");

            entity.HasOne(e => e.Client).WithMany(e => e.Invoices).HasForeignKey(e => e.ClientId).HasConstraintName("FK_Invoices_Clients_ClientId").OnDelete(DeleteBehavior.Restrict);

            entity.HasQueryFilter(e => e.UtcDeletedDate == null);
        });

        modelBuilder.Entity<InvoiceItemEntity>(entity =>
        {
            entity.ToTable("InvoiceItems");

            entity.Property(e => e.InvoiceItemId).HasColumnName("InvoiceItemId").HasColumnType("BIGINT").UseIdentityColumn();
            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceId").HasColumnType("BIGINT").IsRequired();
            entity.Property(e => e.Description).HasColumnName("Description").HasColumnType("NVARCHAR(128)").IsRequired();
            entity.Property(e => e.Quantity).HasColumnName("Quantity").HasColumnType("INT").IsRequired();
            entity.Property(e => e.Amount).HasColumnName("Amount").HasColumnType("DECIMAL(19,4)").HasPrecision(19, 4).IsRequired();
            entity.Property(e => e.UtcCreatedDate).HasColumnName("UtcCreatedDate").HasColumnType("DATETIME2").IsRequired();
            entity.Property(e => e.UtcUpdatedDate).HasColumnName("UtcUpdatedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UtcDeletedDate).HasColumnName("UtcDeletedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("(NULL)");

            entity.HasKey(e => e.InvoiceItemId).IsClustered().HasName("PK_InvoiceItem");

            entity.HasOne(e => e.Invoice).WithMany(e => e.InvoiceItems).HasForeignKey(e => e.InvoiceId).HasConstraintName("FK_InvoiceItem_Invoice_InvoiceId").OnDelete(DeleteBehavior.Cascade);

            entity.HasQueryFilter(e => e.UtcDeletedDate == null);
        });
    }
}
