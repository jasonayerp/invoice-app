using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoice.Api.Data.SqlServer.Configurations;

internal sealed class ClientConfiguration : IEntityTypeConfiguration<ClientEntity>
{
    public void Configure(EntityTypeBuilder<ClientEntity> builder)
    {
        builder.ToTable("Clients");

        builder.Property(e => e.Id).HasColumnName("Id").HasColumnType("BIGINT").UseIdentityColumn();
        builder.Property(e => e.Guid).HasColumnName("Guid").HasColumnType("UNIQUEIDENTIFIER").IsRequired().HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasColumnName("Name").HasColumnType("NVARCHAR(56)").IsRequired();
        builder.Property(e => e.Email).HasColumnName("Email").HasColumnType("NVARCHAR(128)").IsRequired(false).HasDefaultValueSql("NULL");
        builder.Property(e => e.CreatedAt).HasColumnName("CreatedAt").HasColumnType("DATETIMEOFFSET(7)").IsRequired();
        builder.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType("DATETIMEOFFSET(7)").IsRequired(false).HasDefaultValueSql("NULL");
        builder.Property(e => e.DeletedAt).HasColumnName("DeletedAt").HasColumnType("DATETIMEOFFSET(7)").IsRequired(false).HasDefaultValueSql("NULL");

        builder.HasKey(e => e.Id).HasName("PK_Clients");
        builder.HasIndex(e => e.Name).IsClustered(false).IsUnique().HasDatabaseName("UX_Client_Name");

        builder.HasQueryFilter(e => e.DeletedAt == null);
    }
}
