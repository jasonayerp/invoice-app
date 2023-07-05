﻿using Invoice.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Api.Data.MySql;

public class InvoiceDbContext : DbContext
{
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<InvoiceEntity> Invoices { get; set; }
    public DbSet<InvoiceItemEntity> InvoicesItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("server=localhost;uid=root;pwd=Mang0isGreat!;database=invoice");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressEntity>(entity =>
        {
            entity.ToTable("addresses");

            entity.Property(e => e.AddressId).HasColumnName("address_id").HasColumnType("bigint").IsRequired().UseIdentityColumn();
            entity.Property(e => e.Guid).HasColumnName("guid").HasColumnType("varchar(16)").IsRequired().HasDefaultValueSql("uuid()");
            entity.Property(e => e.AddressLine1).HasColumnName("address_line_1").HasColumnType("varchar(128)").IsRequired();
            entity.Property(e => e.AddressLine2).HasColumnName("address_line_2").HasColumnType("varchar(128)").IsRequired(false).HasDefaultValueSql("null");
            entity.Property(e => e.AddressLine3).HasColumnName("address_line_3").HasColumnType("varchar(128)").IsRequired(false).HasDefaultValueSql("null");
            entity.Property(e => e.AddressLine4).HasColumnName("address_line_4").HasColumnType("varchar(128)").IsRequired(false).HasDefaultValueSql("null");
            entity.Property(e => e.City).HasColumnName("city").HasColumnType("varchar(128)").IsRequired();
            entity.Property(e => e.Region).HasColumnName("region").HasColumnType("varchar(128)").IsRequired();
            entity.Property(e => e.PostalCode).HasColumnName("postal_code").HasColumnType("varchar(128)").IsRequired();
            entity.Property(e => e.CountryCode).HasColumnName("country_code").HasColumnType("char(2)").IsRequired();
            entity.Property(e => e.UtcCreatedDate).HasColumnName("utc_created_date").HasColumnType("datetime").IsRequired();
            entity.Property(e => e.UtcUpdatedDate).HasColumnName("utc_updated_date").HasColumnType("datetime").IsRequired(false).HasDefaultValueSql("null");
            entity.Property(e => e.UtcDeletedDate).HasColumnName("utc_deleted_date").HasColumnType("datetime").IsRequired(false).HasDefaultValueSql("null");

            entity.HasKey(e => e.AddressId).IsClustered().HasName("pk_addresses");
            entity.HasIndex(e => e.City).IsClustered(false).HasDatabaseName("ix_addresses_cty");
            entity.HasIndex(e => e.Region).IsClustered(false).HasDatabaseName("ix_addresses_region");
            entity.HasIndex(e => e.CountryCode).IsClustered(false).HasDatabaseName("ix_addresses_country_code");
            entity.HasIndex(e => new { e.AddressLine1, e.City, e.Region, e.PostalCode, e.CountryCode }).IsClustered(false).IsUnique().HasDatabaseName("ux_addresses_address");
        });

        modelBuilder.Entity<ClientEntity>(entity =>
        {
            entity.ToTable("clients");

            entity.Property(e => e.ClientId).HasColumnName("client_id").HasColumnType("bigint").IsRequired().UseIdentityColumn();
            entity.Property(e => e.Guid).HasColumnName("guid").HasColumnType("varchar(16)").IsRequired().HasDefaultValueSql("uuid()");
            entity.Property(e => e.Name).HasColumnName("name").HasColumnType("varchar(50)").IsRequired();
            entity.Property(e => e.Phone).HasColumnName("phone").HasColumnType("varchar(50)").IsRequired(false).HasDefaultValueSql("null");
            entity.Property(e => e.Email).HasColumnName("email").HasColumnType("varchar(50)").IsRequired(false).HasDefaultValueSql("null");
            entity.Property(e => e.UtcCreatedDate).HasColumnName("utc_created_date").HasColumnType("datetime").IsRequired();
            entity.Property(e => e.UtcUpdatedDate).HasColumnName("utc_updated_date").HasColumnType("datetime").IsRequired(false).HasDefaultValueSql("null");
            entity.Property(e => e.UtcDeletedDate).HasColumnName("utc_deleted_date").HasColumnType("datetime").IsRequired(false).HasDefaultValueSql("null");

            entity.HasKey(e => e.ClientId).IsClustered().HasName("pk_clients");
            entity.HasIndex(e => e.Guid).IsClustered(false).HasDatabaseName("Iix_client_guid");
            entity.HasIndex(e => e.Name).IsClustered(false).IsUnique().HasDatabaseName("ux_client_name");
        });

        modelBuilder.Entity<InvoiceEntity>(entity =>
        {
            entity.ToTable("invoices");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id").HasColumnType("BIGINT").IsRequired().UseIdentityColumn();
            entity.Property(e => e.Guid).HasColumnName("guid").HasColumnType("UNIQUEIDENTIFIER").IsRequired().HasDefaultValueSql("uuid()");
            entity.Property(e => e.Number).HasColumnName("number").HasColumnType("varchar(30)").IsRequired();
            entity.Property(e => e.UtcDate).HasColumnName("utc_date").HasColumnType("DATETIME2").IsRequired();
            entity.Property(e => e.Status).HasColumnName("status").HasColumnType("SMALLINT").IsRequired();
            entity.Property(e => e.PaymentTerm).HasColumnName("payment_term").HasColumnType("SMALLINT").IsRequired();
            entity.Property(e => e.BillFromAddressId).HasColumnName("bill_from_address_id").HasColumnType("BIGINT").IsRequired();
            entity.Property(e => e.BillToAddressId).HasColumnName("bill_to_address_id").HasColumnType("BIGINT").IsRequired();
            entity.Property(e => e.ClientId).HasColumnName("client_id").HasColumnType("BIGINT").IsRequired();
            entity.Property(e => e.UtcCreatedDate).HasColumnName("utc_created_date").HasColumnType("datetime").IsRequired();
            entity.Property(e => e.UtcUpdatedDate).HasColumnName("utc_updated_date").HasColumnType("datetime").IsRequired(false).HasDefaultValueSql("null");
            entity.Property(e => e.UtcDeletedDate).HasColumnName("utc_deleted_date").HasColumnType("datetime").IsRequired(false).HasDefaultValueSql("null");

            entity.HasKey(e => e.InvoiceId).IsClustered().HasName("pk_invoices");
            entity.HasIndex(e => e.Number).IsClustered(false).IsUnique().HasDatabaseName("ux_invoices_number");

            entity.HasOne(e => e.BillFromAddress).WithOne().HasForeignKey<InvoiceEntity>(e => e.BillFromAddressId).HasConstraintName("fk_invoices_addresses_bill_from_address_id");
            entity.HasOne(e => e.BillToAddress).WithOne().HasForeignKey<InvoiceEntity>(e => e.BillToAddressId).HasConstraintName("fk_invoices_addresses_bill_ftom_address_id");
            entity.HasOne(e => e.Client).WithMany(e => e.Invoices).HasForeignKey(e => e.ClientId).HasConstraintName("fk_invoices_clients_client_id").OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<InvoiceItemEntity>(entity =>
        {
            entity.ToTable("invoice_items");

            entity.Property(e => e.InvoiceItemId).HasColumnName("invoice_item_id").HasColumnType("bigint").IsRequired().UseIdentityColumn();
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id").HasColumnType("bigint").IsRequired();
            entity.Property(e => e.Description).HasColumnName("description").HasColumnType("varchar(128)").IsRequired();
            entity.Property(e => e.Quantity).HasColumnName("quantity").HasColumnType("int").IsRequired();
            entity.Property(e => e.Amount).HasColumnName("amount").HasColumnType("decimal(19,4)").HasPrecision(19, 4).IsRequired();
            entity.Property(e => e.UtcCreatedDate).HasColumnName("utc_created_date").HasColumnType("datetime").IsRequired();
            entity.Property(e => e.UtcUpdatedDate).HasColumnName("utc_updated_date").HasColumnType("datetime").IsRequired(false).HasDefaultValueSql("null");
            entity.Property(e => e.UtcDeletedDate).HasColumnName("utc_deleted_date").HasColumnType("datetime").IsRequired(false).HasDefaultValueSql("null");

            entity.HasKey(e => e.InvoiceItemId).IsClustered().HasName("pk_invoice_item");
            entity.HasIndex(e => e.Description).IsClustered(false).IsUnique().HasDatabaseName("ux_invoice_items_description");

            entity.HasOne(e => e.Invoice).WithMany(e => e.InvoiceItems).HasForeignKey(e => e.InvoiceId).HasConstraintName("fk_invoice_item_invoice_invoice_id").OnDelete(DeleteBehavior.Cascade);
        });
    }
}
