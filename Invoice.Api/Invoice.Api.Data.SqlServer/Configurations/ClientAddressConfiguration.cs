using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoice.Api.Data.SqlServer.Configurations;

internal sealed class ClientAddressConfiguration : IEntityTypeConfiguration<ClientAddressEntity>
{
    public void Configure(EntityTypeBuilder<ClientAddressEntity> builder)
    {
        builder.ToTable("ClientAddresses");

        builder.Property(e => e.Id).HasColumnName("ClientAddressId").HasColumnType("BIGINT").UseIdentityColumn();
        builder.Property(e => e.ClientId).HasColumnName("ClientId").HasColumnType("BIGINT").IsRequired();
        builder.Property(e => e.AddressLine1).HasColumnName("AddressLine1").HasColumnType("NVARCHAR(128)").IsRequired();
        builder.Property(e => e.AddressLine2).HasColumnName("AddressLine2").HasColumnType("NVARCHAR(128)").IsRequired(false).HasDefaultValueSql("NULL");
        builder.Property(e => e.AddressLine3).HasColumnName("AddressLine3").HasColumnType("NVARCHAR(128)").IsRequired(false).HasDefaultValueSql("NULL");
        builder.Property(e => e.AddressLine4).HasColumnName("AddressLine4").HasColumnType("NVARCHAR(128)").IsRequired(false).HasDefaultValueSql("NULL");
        builder.Property(e => e.City).HasColumnName("City").HasColumnType("NVARCHAR(96)").IsRequired();
        builder.Property(e => e.Region).HasColumnName("Region").HasColumnType("NVARCHAR(96)").IsRequired(false).HasDefaultValueSql("NULL");
        builder.Property(e => e.PostalCode).HasColumnName("PostalCode").HasColumnType("NVARCHAR(8)").IsRequired();
        builder.Property(e => e.CountryCode).HasColumnName("CountryCode").HasColumnType("NCHAR(2)").IsRequired();
        builder.Property(e => e.IsActive).HasColumnName("IsActive").HasColumnType("BIT").IsRequired();
        builder.Property(e => e.IsPrimary).HasColumnName("IsPrimary").HasColumnType("BIT").IsRequired();
        builder.Property(e => e.CreatedAt).HasColumnName("CreatedAt").HasColumnType("DATETIMEOFFSET(7)").IsRequired();
        builder.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType("DATETIMEOFFSET(7)").IsRequired(false).HasDefaultValueSql("NULL");
        builder.Property(e => e.DeletedAt).HasColumnName("DeletedAt").HasColumnType("DATETIMEOFFSET(7)").IsRequired(false).HasDefaultValueSql("NULL");

        builder.HasKey(e => e.Id).HasName("PK_Addresses");
        builder.HasIndex(e => e.City).IsClustered(false).HasDatabaseName("IX_Addresses_City");
        builder.HasIndex(e => e.Region).IsClustered(false).HasDatabaseName("IX_Addresses_Region");
        builder.HasIndex(e => e.CountryCode).IsClustered(false).HasDatabaseName("IX_Addresses_CountryCode");
        builder.HasIndex(e => new { e.ClientId, e.AddressLine1, e.AddressLine2, e.AddressLine3, e.AddressLine4, e.City, e.Region, e.PostalCode, e.CountryCode }).IsClustered(false).IsUnique().HasDatabaseName("UX_ClientAddresses_Address");

        builder.HasOne(e => e.Client).WithMany(e => e.Addresses).HasForeignKey(e => e.ClientId).HasConstraintName("FK_ClientAddresses_Clients_ClientId").OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(e => e.DeletedAt == null);
    }
}
