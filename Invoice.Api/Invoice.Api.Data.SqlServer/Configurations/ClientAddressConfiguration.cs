using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoice.Api.Data.SqlServer.Configurations;

internal sealed class ClientAddressConfiguration : IEntityTypeConfiguration<ClientAddressEntity>
{
    public void Configure(EntityTypeBuilder<ClientAddressEntity> builder)
    {
        builder.ToTable("ClientAddresses");

        builder.Property(e => e.Id).HasColumnName("ClientAddressId").HasColumnType("BIGINT").UseIdentityColumn();
        builder.Property(e => e.ClientId).HasColumnName("ClientId").HasColumnType("BIGINT");
        builder.Property(e => e.Line1).HasColumnName("Line1").HasColumnType("NVARCHAR(128)");
        builder.Property(e => e.Line2).HasColumnName("Line2").HasColumnType("NVARCHAR(128)").IsRequired(false).HasDefaultValueSql("NULL");
        builder.Property(e => e.Line3).HasColumnName("Line3").HasColumnType("NVARCHAR(128)").IsRequired(false).HasDefaultValueSql("NULL");
        builder.Property(e => e.Line4).HasColumnName("Line4").HasColumnType("NVARCHAR(128)").IsRequired(false).HasDefaultValueSql("NULL");
        builder.Property(e => e.City).HasColumnName("City").HasColumnType("NVARCHAR(96)");
        builder.Property(e => e.Region).HasColumnName("Region").HasColumnType("NVARCHAR(96)").IsRequired(false).HasDefaultValueSql("NULL");
        builder.Property(e => e.PostalCode).HasColumnName("PostalCode").HasColumnType("NVARCHAR(8)");
        builder.Property(e => e.CountryCode).HasColumnName("CountryCode").HasColumnType("NCHAR(2)");
        builder.Property(e => e.IsDefault).HasColumnName("IsDefault").HasColumnType("BIT");
        builder.Property(e => e.CreatedAt).HasColumnName("CreatedAt").HasColumnType("DATETIMEOFFSET(7)");
        builder.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType("DATETIMEOFFSET(7)").IsRequired(false).HasDefaultValueSql("NULL");
        builder.Property(e => e.DeletedAt).HasColumnName("DeletedAt").HasColumnType("DATETIMEOFFSET(7)").IsRequired(false).HasDefaultValueSql("NULL");

        builder.HasKey(e => e.Id).HasName("PK_Addresses");
        builder.HasIndex(e => new { e.ClientId, e.IsDefault }).IsClustered(false).IsUnique().HasDatabaseName("UX_ClientAddresses_DefaultAddress");
        builder.HasIndex(e => new { e.ClientId, e.Line1, e.Line2, e.Line3, e.Line4, e.City, e.Region, e.PostalCode, e.CountryCode }).IsClustered(false).IsUnique().HasDatabaseName("UX_ClientAddresses_Address");

        builder.HasOne(e => e.Client).WithMany(e => e.Addresses).HasForeignKey(e => e.ClientId).HasConstraintName("FK_ClientAddresses_Clients_ClientId").OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(e => e.DeletedAt == null);
    }
}
