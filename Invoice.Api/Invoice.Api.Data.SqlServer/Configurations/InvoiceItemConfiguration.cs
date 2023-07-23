namespace Invoice.Api.Data.SqlServer.Configurations;

internal sealed class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItemEntity>
{
    public void Configure(EntityTypeBuilder<InvoiceItemEntity> builder)
    {
        builder.ToTable("InvoiceItems");

        builder.Property(e => e.Id).HasColumnName("InvoiceItemId").HasColumnType("BIGINT").UseIdentityColumn();
        builder.Property(e => e.InvoiceId).HasColumnName("InvoiceId").HasColumnType("BIGINT");
        builder.Property(e => e.Description).HasColumnName("Description").HasColumnType("NVARCHAR(128)");
        builder.Property(e => e.Quantity).HasColumnName("Quantity").HasColumnType("INT").IsRequired();
        builder.Property(e => e.UnitPrice).HasColumnName("UnitPrice").HasColumnType("DECIMAL(19, 4)").HasPrecision(19, 4);
        builder.Property(e => e.TotalPrice).HasColumnName("TotalPrice").HasComputedColumnSql("[Quantity] * [UnitPrice]");
        builder.Property(e => e.CreatedAt).HasColumnName("CreatedAt").HasColumnType("DATETIMEOFFSET(7)");
        builder.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType("DATETIMEOFFSET(7)").IsRequired(false).HasDefaultValueSql("NULL");
        builder.Property(e => e.DeletedAt).HasColumnName("DeletedAt").HasColumnType("DATETIMEOFFSET(7)").IsRequired(false).HasDefaultValueSql("NULL");

        builder.HasKey(e => e.Id).HasName("PK_InvoiceItems");

        builder.HasOne(e => e.Invoice).WithMany(e => e.InvoiceItems).HasForeignKey(e => e.InvoiceId).HasConstraintName("FK_InvoiceItems_Invoices_InvoiceId").OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(e => e.DeletedAt == null);
    }
}
