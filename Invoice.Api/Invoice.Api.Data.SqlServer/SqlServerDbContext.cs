using Invoice.Api.Data.Documents;
using Invoice.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Api.Data.SqlServer;

public class SqlServerDbContext : DbContext
{
    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<ClientAddressEntity> ClientAddresses { get; set; }
    public DbSet<ClientPhoneEntity> ClientPhones { get; set; }
    public DbSet<InvoiceEntity> Invoices { get; set; }
    public DbSet<InvoiceItemEntity> InvoicesItems { get; set; }

    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.Entity<ClientEntity>(entity =>
        {
            entity.ToTable("Clients");

            entity.Property(e => e.ClientId).HasColumnName("ClientId").HasColumnType("BIGINT").IsRequired().UseIdentityColumn();
            entity.Property(e => e.Guid).HasColumnName("Guid").HasColumnType("UNIQUEIDENTIFIER").IsRequired().HasDefaultValueSql("(NEWID())").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("Name").HasColumnType("NVARCHAR(50)").IsRequired();
            entity.Property(e => e.Email).HasColumnName("Email").HasColumnType("NVARCHAR(50)").IsRequired(false).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UtcCreatedDate).HasColumnName("UtcCreatedDate").HasColumnType("DATETIME2").IsRequired();
            entity.Property(e => e.UtcUpdatedDate).HasColumnName("UtcUpdatedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UtcDeletedDate).HasColumnName("UtcDeletedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("(NULL)");

            entity.HasKey(e => e.ClientId).IsClustered().HasName("PK_Clients");
            entity.HasIndex(e => e.Guid).IsClustered(false).HasDatabaseName("IX_Client_Guid");
            entity.HasIndex(e => e.Name).IsClustered(false).IsUnique().HasDatabaseName("UX_Client_Name");

            entity.HasQueryFilter(e => e.UtcDeletedDate == null);

            IEnumerable<ClientEntity> data = Enumerable.Empty<ClientEntity>();
            data.Append(new ClientEntity
            {
                ClientId = 1,
                Name = "Jensen Huang",
                Email = "jensenh@mail.com",
                UtcCreatedDate = new DateTime(2018, 2, 18)
            });
            data.Append(new ClientEntity
            {
                ClientId = 2,
                Name = "Alex Grim",
                Email = "alexgrim@mail.com",
                UtcCreatedDate = new DateTime(2018, 2, 18)
            });

            entity.HasData(data);
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
            entity.Property(e => e.City).HasColumnName("City").HasColumnType("NVARCHAR(128)").IsRequired();
            entity.Property(e => e.Region).HasColumnName("Region").HasColumnType("NVARCHAR(128)").IsRequired(false).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.PostalCode).HasColumnName("PostalCode").HasColumnType("NVARCHAR(128)").IsRequired();
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
            entity.HasIndex(e => new { e.AddressLine1, e.City, e.Region, e.PostalCode, e.CountryCode }).IsClustered(false).IsUnique().HasDatabaseName("UX_Addresses_Address");

            entity.HasOne(e => e.Client).WithMany(e => e.Addresses).HasForeignKey(e => e.ClientId).HasConstraintName("FK_ClientAddresses_Clients_ClientId").OnDelete(DeleteBehavior.Cascade);

            entity.HasQueryFilter(e => e.UtcDeletedDate == null);

            IEnumerable<ClientAddressEntity> data = Enumerable.Empty<ClientAddressEntity>();
            data.Append(new ClientAddressEntity
            {
                ClientAddressId = 1,
                ClientId = 1,
                AddressLine1 = "106 Kendell Street",
                City = "Sharrington",
                PostalCode = "NR24 5WQ",
                CountryCode = "UK",
                IsActive = true,
                IsPrimary = true,
                UtcCreatedDate = new DateTime(2018, 2, 18)
            });
            data.Append(new ClientAddressEntity
            {
                ClientAddressId = 2,
                ClientId = 2,
                AddressLine1 = "84 Church Way",
                City = "Bradford",
                PostalCode = "BD1 9PB",
                CountryCode = "UK",
                IsActive = true,
                IsPrimary = true,
                UtcCreatedDate = new DateTime(2018, 2, 18)
            });

            entity.HasData(data);
        });        

        modelBuilder.Entity<ClientPhoneEntity>(entity =>
        {
            entity.ToTable("ClientPhones");

            entity.Property(e => e.ClientPhoneId).HasColumnName("ClientPhoneId").HasColumnType("BIGINTT").UseIdentityColumn();
            entity.Property(e => e.ClientId).HasColumnName("ClientId").HasColumnType("BIGINT").IsRequired();
            entity.Property(e => e.UtcCreatedDate).HasColumnName("UtcCreatedDate").HasColumnType("DATETIME2").IsRequired();
            entity.Property(e => e.UtcUpdatedDate).HasColumnName("UtcUpdatedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UtcDeletedDate).HasColumnName("UtcDeletedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("(NULL)");

            entity.HasOne(e => e.Client).WithMany(e => e.Phones).HasForeignKey(e => e.ClientId).HasConstraintName("FK_ClientPhones_Clients_ClientId").OnDelete(DeleteBehavior.Cascade);

            entity.HasKey(e => e.ClientPhoneId).HasName("PK_ClientPhones");

            entity.HasQueryFilter(e => e.UtcDeletedDate == null);
        });

        modelBuilder.Entity<InvoiceEntity>(entity =>
        {
            entity.ToTable("Invoices");

            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceId").HasColumnType("BIGINT").UseIdentityColumn();
            entity.Property(e => e.Guid).HasColumnName("Guid").HasColumnType("UNIQUEIDENTIFIER").IsRequired().HasDefaultValueSql("(NEWID())").ValueGeneratedOnAdd();
            entity.Property(e => e.ClientId).HasColumnName("ClientId").HasColumnType("BIGINT").IsRequired();
            entity.Property(e => e.Number).HasColumnName("Number").HasColumnType("NVARCHAR(30)").IsRequired();
            entity.Property(e => e.Description).HasColumnName("Description").HasColumnType("NVARCHAR(128)").IsRequired();
            entity.Property(e => e.UtcDate).HasColumnName("UtcDate").HasColumnType("DATE").IsRequired();
            entity.Property(e => e.UtcDueDate).HasColumnName("UtcDueDate").HasColumnType("DATE").IsRequired();
            entity.Property(e => e.Status).HasColumnName("Status").HasColumnType("SMALLINT").IsRequired();
            entity.Property(e => e.PaymentTerm).HasColumnName("PaymentTerm").HasColumnType("SMALLINT").IsRequired();
            entity.Property(e => e.UtcCreatedDate).HasColumnName("UtcCreatedDate").HasColumnType("DATETIME2").IsRequired();
            entity.Property(e => e.UtcUpdatedDate).HasColumnName("UtcUpdatedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UtcDeletedDate).HasColumnName("UtcDeletedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("(NULL)");

            entity.HasKey(e => e.InvoiceId).IsClustered().HasName("PK_Invoices");
            entity.HasIndex(e => e.Number).IsClustered(false).IsUnique().HasDatabaseName("UX_Invoices_Number");

            entity.HasOne(e => e.Client).WithMany(e => e.Invoices).HasForeignKey(e => e.ClientId).HasConstraintName("FK_Invoices_Clients_ClientId").OnDelete(DeleteBehavior.Restrict);

            entity.HasQueryFilter(e => e.UtcDeletedDate == null);

            IEnumerable<InvoiceEntity> data = Enumerable.Empty<InvoiceEntity>();
            data.Append(new InvoiceEntity
            {
                InvoiceId = 1,
                ClientId = 1,
                Number = "RT3080",
                Description = "Re-branding",
                UtcDate = new DateTime(2021, 8, 11),
                UtcDueDate = new DateTime(2021, 8, 19),
                PaymentTerm = 1,
                Status = 1,
                UtcCreatedDate = new DateTime(2021, 8, 11)
            });
            data.Append(new InvoiceEntity
            {
                InvoiceId = 2,
                ClientId = 2,
                Number = "XM9141",
                Description = "Re-branding",
                UtcDate = new DateTime(2021, 8, 11),
                UtcDueDate = new DateTime(2021, 8, 19),
                PaymentTerm = 1,
                Status = 1,
                UtcCreatedDate = new DateTime(2021, 8, 11)
            });

            entity.HasData(data);
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
            entity.HasIndex(e => e.Description).IsClustered(false).IsUnique().HasDatabaseName("UX_InvoiceItems_Description");

            entity.HasOne(e => e.Invoice).WithMany(e => e.InvoiceItems).HasForeignKey(e => e.InvoiceId).HasConstraintName("FK_InvoiceItem_Invoice_InvoiceId").OnDelete(DeleteBehavior.Cascade);

            entity.HasQueryFilter(e => e.UtcDeletedDate == null);

            IEnumerable<InvoiceItemEntity> data = Enumerable.Empty<InvoiceItemEntity>();
            data.Append(new InvoiceItemEntity
            {
                InvoiceItemId = 1,
                InvoiceId = 1,
                Description = "Brand Guidelines",
                Quantity = 1,
                Amount = 1800.90m
            });

            entity.HasData(data);
        });
    }
}
