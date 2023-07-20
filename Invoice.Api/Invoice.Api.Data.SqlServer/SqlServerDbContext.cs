using Microsoft.EntityFrameworkCore;
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
            entity.Property(e => e.Guid).HasColumnName("Guid").HasColumnType("UNIQUEIDENTIFIER").IsRequired().HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("Name").HasColumnType("NVARCHAR(56)").IsRequired();
            entity.Property(e => e.Email).HasColumnName("Email").HasColumnType("NVARCHAR(128)").IsRequired(false).HasDefaultValueSql("NULL");
            entity.Property(e => e.UtcCreatedDate).HasColumnName("UtcCreatedDate").HasColumnType("DATETIME2").IsRequired();
            entity.Property(e => e.UtcUpdatedDate).HasColumnName("UtcUpdatedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("NULL");
            entity.Property(e => e.UtcDeletedDate).HasColumnName("UtcDeletedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("NULL");

            entity.HasKey(e => e.ClientId).HasName("PK_Clients");
            entity.HasIndex(e => e.Name).IsClustered(false).IsUnique().HasDatabaseName("UX_Client_Name");

            entity.HasQueryFilter(e => e.UtcDeletedDate == null);

            IEnumerable<ClientEntity> data = Enumerable.Empty<ClientEntity>();
            data.Append(new ClientEntity
            {
                ClientId = 1,
                Guid = Guid.NewGuid(),
                Name = "Jensen Huang",
                Email = "jensenh@mail.com",
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new ClientEntity
            {
                ClientId = 2,
                Guid = Guid.NewGuid(),
                Name = "Alex Grim",
                Email = "alexgrim@mail.com",
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new ClientEntity
            {
                ClientId = 3,
                Guid = Guid.NewGuid(),
                Name = "John Morrison",
                Email = "jm@myco.com",
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new ClientEntity
            {
                ClientId = 4,
                Guid = Guid.NewGuid(),
                Name = "Alysa Werner",
                Email = "alysa@email.co.uk",
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new ClientEntity
            {
                ClientId = 5,
                Guid = Guid.NewGuid(),
                Name = "Mellisa Clarke",
                Email = "mellisa.clarke@example.com",
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new ClientEntity
            {
                ClientId = 6,
                Guid = Guid.NewGuid(),
                Name = "Thomas Wayne",
                Email = "thomas@dc.com",
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new ClientEntity
            {
                ClientId = 7,
                Guid = Guid.NewGuid(),
                Name = "Anita Wainwright",
                Email = null,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });

            entity.HasData(data);
        });

        modelBuilder.Entity<ClientAddressEntity>(entity =>
        {
            entity.ToTable("ClientAddresses");

            entity.Property(e => e.ClientAddressId).HasColumnName("ClientAddressId").HasColumnType("BIGINT").UseIdentityColumn();
            entity.Property(e => e.ClientId).HasColumnName("ClientId").HasColumnType("BIGINT").IsRequired();
            entity.Property(e => e.AddressLine1).HasColumnName("AddressLine1").HasColumnType("NVARCHAR(128)").IsRequired();
            entity.Property(e => e.AddressLine2).HasColumnName("AddressLine2").HasColumnType("NVARCHAR(128)").IsRequired(false).HasDefaultValueSql("NULL");
            entity.Property(e => e.AddressLine3).HasColumnName("AddressLine3").HasColumnType("NVARCHAR(128)").IsRequired(false).HasDefaultValueSql("NULL");
            entity.Property(e => e.AddressLine4).HasColumnName("AddressLine4").HasColumnType("NVARCHAR(128)").IsRequired(false).HasDefaultValueSql("NULL");
            entity.Property(e => e.City).HasColumnName("City").HasColumnType("NVARCHAR(96)").IsRequired();
            entity.Property(e => e.Region).HasColumnName("Region").HasColumnType("NVARCHAR(96)").IsRequired(false).HasDefaultValueSql("NULL");
            entity.Property(e => e.PostalCode).HasColumnName("PostalCode").HasColumnType("NVARCHAR(8)").IsRequired();
            entity.Property(e => e.CountryCode).HasColumnName("CountryCode").HasColumnType("NCHAR(2)").IsRequired();
            entity.Property(e => e.IsActive).HasColumnName("IsActive").HasColumnType("BIT").IsRequired();
            entity.Property(e => e.IsPrimary).HasColumnName("IsPrimary").HasColumnType("BIT").IsRequired();
            entity.Property(e => e.UtcCreatedDate).HasColumnName("UtcCreatedDate").HasColumnType("DATETIME2").IsRequired();
            entity.Property(e => e.UtcUpdatedDate).HasColumnName("UtcUpdatedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("NULL");
            entity.Property(e => e.UtcDeletedDate).HasColumnName("UtcDeletedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("NULL");

            entity.HasKey(e => e.ClientAddressId).HasName("PK_Addresses");
            entity.HasIndex(e => e.City).IsClustered(false).HasDatabaseName("IX_Addresses_City");
            entity.HasIndex(e => e.Region).IsClustered(false).HasDatabaseName("IX_Addresses_Region");
            entity.HasIndex(e => e.CountryCode).IsClustered(false).HasDatabaseName("IX_Addresses_CountryCode");
            entity.HasIndex(e => new { e.ClientId, e.AddressLine1, e.AddressLine2, e.AddressLine3, e.AddressLine4, e.City, e.Region, e.PostalCode, e.CountryCode }).IsClustered(false).IsUnique().HasDatabaseName("UX_ClientAddresses_Address");

            entity.HasOne(e => e.Client).WithMany(e => e.Addresses).HasForeignKey(e => e.ClientId).HasConstraintName("FK_ClientAddresses_Clients_ClientId").OnDelete(DeleteBehavior.Cascade);

            entity.HasQueryFilter(e => e.UtcDeletedDate == null);

            IEnumerable < ClientAddressEntity> data = Enumerable.Empty<ClientAddressEntity>();
            data.Append(new ClientAddressEntity
            {
                ClientAddressId = 1,
                ClientId = 1,
                AddressLine1 = "106 Kendell Street",
                AddressLine2 = null,
                AddressLine3 = null,
                AddressLine4 = null,
                City = "Sharrington",
                Region = null,
                PostalCode = "NR24 5WQ",
                CountryCode = "UK",
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new ClientAddressEntity
            {
                ClientAddressId = 2,
                ClientId = 2,
                AddressLine1 = "84 Church Way",
                AddressLine2 = null,
                AddressLine3 = null,
                AddressLine4 = null,
                City = "Bradford",
                Region = null,
                PostalCode = "BD1 9PB",
                CountryCode = "UK",
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new ClientAddressEntity
            {
                ClientAddressId = 3,
                ClientId = 3,
                AddressLine1 = "79 Dover Road",
                AddressLine2 = null,
                AddressLine3 = null,
                AddressLine4 = null,
                City = "Westhall",
                Region = null,
                PostalCode = "IP19 3PF",
                CountryCode = "UK",
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new ClientAddressEntity
            {
                ClientAddressId = 4,
                ClientId = 4,
                AddressLine1 = "63 Warwick Road",
                AddressLine2 = null,
                AddressLine3 = null,
                AddressLine4 = null,
                City = "Carlisle",
                Region = null,
                PostalCode = "CA20 2TG",
                CountryCode = "UK",
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new ClientAddressEntity
            {
                ClientAddressId = 5,
                ClientId = 5,
                AddressLine1 = "46 Abbey Row",
                AddressLine2 = null,
                AddressLine3 = null,
                AddressLine4 = null,
                City = "Cambridge",
                Region = null,
                PostalCode = "CB5 6EG",
                CountryCode = "UK",
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new ClientAddressEntity
            {
                ClientAddressId = 6,
                ClientId = 6,
                AddressLine1 = "3964 Queens Lane",
                AddressLine2 = null,
                AddressLine3 = null,
                AddressLine4 = null,
                City = "Gotham",
                Region = "NY",
                PostalCode = "60457",
                CountryCode = "US",
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new ClientAddressEntity
            {
                ClientAddressId = 7,
                ClientId = 6,
                AddressLine1 = "7345 Waycrest Manor",
                AddressLine2 = null,
                AddressLine3 = null,
                AddressLine4 = null,
                City = "Mobile",
                Region = "AL",
                PostalCode = "60457",
                CountryCode = "US",
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });

            entity.HasData(data);
        });

        modelBuilder.Entity<InvoiceEntity>(entity =>
        {
            entity.ToTable("Invoices");

            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceId").HasColumnType("BIGINT").UseIdentityColumn();
            entity.Property(e => e.Guid).HasColumnName("Guid").HasColumnType("UNIQUEIDENTIFIER").IsRequired().HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();
            entity.Property(e => e.ClientId).HasColumnName("ClientId").HasColumnType("BIGINT").IsRequired();
            entity.Property(e => e.Number).HasColumnName("Number").HasColumnType("NVARCHAR(8)").IsRequired();
            entity.Property(e => e.Description).HasColumnName("Description").HasColumnType("NVARCHAR(128)").IsRequired();
            entity.Property(e => e.UtcDate).HasColumnName("UtcDate").HasColumnType("DATE").IsRequired();
            entity.Property(e => e.UtcDueDate).HasColumnName("UtcDueDate").HasColumnType("DATE").IsRequired();
            entity.Property(e => e.Status).HasColumnName("Status").HasColumnType("SMALLINT").IsRequired();
            entity.Property(e => e.PaymentTermDays).HasColumnName("PaymentTermDays").HasColumnType("SMALLINT").IsRequired();
            entity.Property(e => e.UtcCreatedDate).HasColumnName("UtcCreatedDate").HasColumnType("DATETIME2").IsRequired();
            entity.Property(e => e.UtcUpdatedDate).HasColumnName("UtcUpdatedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("NULL");
            entity.Property(e => e.UtcDeletedDate).HasColumnName("UtcDeletedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("NULL");

            entity.HasKey(e => e.InvoiceId).HasName("PK_Invoices");
            entity.HasIndex(e => new { e.ClientId, e.Number }).IsClustered(false).IsUnique().HasDatabaseName("UX_Invoices_ClientId_Number");
            entity.HasIndex(e => e.UtcDate).IsClustered(false).HasDatabaseName("IX_Invoices_UtcDate");
            entity.HasIndex(e => e.UtcDueDate).IsClustered(false).HasDatabaseName("IX_Invoices_UtcDueDate");

            entity.HasOne(e => e.Client).WithMany(e => e.Invoices).HasForeignKey(e => e.ClientId).HasConstraintName("FK_Invoices_Clients_ClientId").OnDelete(DeleteBehavior.Restrict);

            entity.HasQueryFilter(e => e.UtcDeletedDate == null);

            IEnumerable<InvoiceEntity> data = Enumerable.Empty<InvoiceEntity>();
            data.Append(new InvoiceEntity
            {
                InvoiceId = 1,
                Guid = Guid.NewGuid(),
                ClientId = 1,
                Number = "RT3080",
                Description = "Re-branding",
                UtcDate = new DateTime(2021, 8, 18),
                UtcDueDate = new DateTime(2021, 8, 19),
                Status = 1,
                PaymentTermDays = 1,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new InvoiceEntity
            {
                InvoiceId = 2,
                Guid = Guid.NewGuid(),
                ClientId = 2,
                Number = "XM9141",
                Description = "Graphic Design",
                UtcDate = new DateTime(2021, 8, 21),
                UtcDueDate = new DateTime(2021, 9, 20),
                Status = 2,
                PaymentTermDays = 4,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new InvoiceEntity
            {
                InvoiceId = 3,
                Guid = Guid.NewGuid(),
                ClientId = 3,
                Number = "RG0314",
                Description = "Website Redesign",
                UtcDate = new DateTime(2021, 9, 24),
                UtcDueDate = new DateTime(2021, 10, 1),
                Status = 1,
                PaymentTermDays = 7,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new InvoiceEntity
            {
                InvoiceId = 4,
                Guid = Guid.NewGuid(),
                ClientId = 4,
                Number = "RT2080",
                Description = "Logo Concept",
                UtcDate = new DateTime(2021, 10, 11),
                UtcDueDate = new DateTime(2021, 10, 12),
                Status = 2,
                PaymentTermDays = 1,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new InvoiceEntity
            {
                InvoiceId = 5,
                Guid = Guid.NewGuid(),
                ClientId = 5,
                Number = "AA1449",
                Description = "Re-branding",
                UtcDate = new DateTime(2021, 10, 7),
                UtcDueDate = new DateTime(2021, 10, 14),
                Status = 2,
                PaymentTermDays = 7,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new InvoiceEntity
            {
                InvoiceId = 6,
                Guid = Guid.NewGuid(),
                ClientId = 6,
                Number = "TY9141",
                Description = "Landing Page Design",
                UtcDate = new DateTime(2021, 10, 1),
                UtcDueDate = new DateTime(2021, 10, 31),
                Status = 2,
                PaymentTermDays = 30,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new InvoiceEntity
            {
                InvoiceId = 7,
                Guid = Guid.NewGuid(),
                ClientId = 7,
                Number = "FV2353",
                Description = "Logo Re-design",
                UtcDate = new DateTime(2021, 11, 5),
                UtcDueDate = new DateTime(2021, 11, 12),
                Status = 0,
                PaymentTermDays = 7,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
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
            entity.Property(e => e.Price).HasColumnName("Price").HasColumnType("DECIMAL(19,4)").HasPrecision(19, 4).IsRequired();
            entity.Property(e => e.UtcCreatedDate).HasColumnName("UtcCreatedDate").HasColumnType("DATETIME2").IsRequired();
            entity.Property(e => e.UtcUpdatedDate).HasColumnName("UtcUpdatedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("NULL");
            entity.Property(e => e.UtcDeletedDate).HasColumnName("UtcDeletedDate").HasColumnType("DATETIME2").IsRequired(false).HasDefaultValueSql("NULL");

            entity.HasKey(e => e.InvoiceItemId).HasName("PK_InvoiceItem");

            entity.HasOne(e => e.Invoice).WithMany(e => e.InvoiceItems).HasForeignKey(e => e.InvoiceId).HasConstraintName("FK_InvoiceItem_Invoice_InvoiceId").OnDelete(DeleteBehavior.Cascade);

            entity.HasQueryFilter(e => e.UtcDeletedDate == null);

            IEnumerable<InvoiceItemEntity> data = Enumerable.Empty<InvoiceItemEntity>();
            data.Append(new InvoiceItemEntity
            {
                InvoiceItemId = 1,
                InvoiceId = 1,
                Description = "Brand Guidelines",
                Quantity = 1,
                Price = 1800.90m,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new InvoiceItemEntity
            {
                InvoiceItemId = 2,
                InvoiceId = 2,
                Description = "Banner Design",
                Quantity = 1,
                Price = 156.00m,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new InvoiceItemEntity
            {
                InvoiceItemId = 3,
                InvoiceId = 2,
                Description = "Email Design",
                Quantity = 2,
                Price = 200.00m,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new InvoiceItemEntity
            {
                InvoiceItemId = 4,
                InvoiceId = 3,
                Description = "Website Redesign",
                Quantity = 1,
                Price = 14002.33m,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new InvoiceItemEntity
            {
                InvoiceItemId = 5,
                InvoiceId = 4,
                Description = "Logo Sketches",
                Quantity = 1,
                Price = 102.04m,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new InvoiceItemEntity
            {
                InvoiceItemId = 6,
                InvoiceId = 5,
                Description = "New Logo",
                Quantity = 1,
                Price = 1532.33m,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new InvoiceItemEntity
            {
                InvoiceItemId = 7,
                InvoiceId = 5,
                Description = "Brand Guidelines",
                Quantity = 1,
                Price = 2500.00m,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new InvoiceItemEntity
            {
                InvoiceItemId = 8,
                InvoiceId = 6,
                Description = "Web Design",
                Quantity = 1,
                Price = 6155.91m,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });
            data.Append(new InvoiceItemEntity
            {
                InvoiceItemId = 9,
                InvoiceId = 7,
                Description = "Logo Re-design",
                Quantity = 1,
                Price = 3102.94m,
                UtcCreatedDate = DateTime.UtcNow,
                UtcUpdatedDate = null,
                UtcDeletedDate = null
            });

            entity.HasData(data);
        });
    }
}
