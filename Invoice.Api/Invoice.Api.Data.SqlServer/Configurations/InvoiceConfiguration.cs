using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoice.Api.Data.SqlServer.Configurations;

internal sealed class InvoiceConfiguration : IEntityTypeConfiguration<InvoiceEntity>
{
    public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
    {
        builder.ToTable("Invoices");

        builder.Property(e => e.Id).HasColumnName("InvoiceId").HasColumnType("BIGINT").UseIdentityColumn();
        builder.Property(e => e.Guid).HasColumnName("Guid").HasColumnType("UNIQUEIDENTIFIER").HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();
        builder.Property(e => e.ClientId).HasColumnName("ClientId").HasColumnType("BIGINT").IsRequired();
        builder.Property(e => e.Number).HasColumnName("Number").HasColumnType("NVARCHAR(8)").IsRequired();
        builder.Property(e => e.Description).HasColumnName("Description").HasColumnType("NVARCHAR(128)").IsRequired();
        builder.Property(e => e.Date).HasColumnName("Date").HasColumnType("DATE").IsRequired();
        builder.Property(e => e.DueDate).HasColumnName("DueDate").HasColumnType("DATE").IsRequired();
        builder.Property(e => e.Status).HasColumnName("Status").HasColumnType("SMALLINT(1)").IsRequired();
        builder.Property(e => e.PaymentTermDays).HasColumnName("PaymentTermDays").HasColumnType("SMALLINT(2)").IsRequired();
        builder.Property(e => e.CreatedAt).HasColumnName("CreatedAt").HasColumnType("DATETIMEOFFSET(7)").IsRequired();
        builder.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType("DATETIMEOFFSET(7)").IsRequired(false).HasDefaultValueSql("NULL");
        builder.Property(e => e.DeletedAt).HasColumnName("DeletedAt").HasColumnType("DATETIMEOFFSET(7)").IsRequired(false).HasDefaultValueSql("NULL");

        builder.HasKey(e => e.Id).HasName("PK_Invoices");
        builder.HasIndex(e => new { e.ClientId, e.Number }).IsClustered(false).IsUnique().HasDatabaseName("UX_Invoices_ClientId_Number");
        builder.HasIndex(e => e.Date).IsClustered(false).HasDatabaseName("IX_Invoices_Date");
        builder.HasIndex(e => e.DueDate).IsClustered(false).HasDatabaseName("IX_Invoices_DueDate");

        builder.HasOne(e => e.Client).WithMany(e => e.Invoices).HasForeignKey(e => e.ClientId).HasConstraintName("FK_Invoices_Clients_ClientId").OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(e => e.DeletedAt == null);
    }
}
