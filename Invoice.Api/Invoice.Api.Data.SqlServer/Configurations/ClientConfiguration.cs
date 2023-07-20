using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoice.Api.Data.SqlServer.Configurations;

internal sealed class ClientConfiguration : IEntityTypeConfiguration<ClientEntity>
{
    public void Configure(EntityTypeBuilder<ClientEntity> entity)
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
    }
}
