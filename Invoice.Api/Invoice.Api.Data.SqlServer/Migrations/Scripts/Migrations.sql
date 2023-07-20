IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720040448_Init_Database')
BEGIN
    CREATE TABLE [dbo].[Clients] (
        [ClientId] BIGINT NOT NULL IDENTITY,
        [Guid] UNIQUEIDENTIFIER NOT NULL DEFAULT (NEWID()),
        [Name] NVARCHAR(56) NOT NULL,
        [Email] NVARCHAR(128) NULL DEFAULT (NULL),
        [UtcCreatedDate] DATETIME2 NOT NULL,
        [UtcUpdatedDate] DATETIME2 NULL DEFAULT (NULL),
        [UtcDeletedDate] DATETIME2 NULL DEFAULT (NULL),
        CONSTRAINT [PK_Clients] PRIMARY KEY ([ClientId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720040448_Init_Database')
BEGIN
    CREATE TABLE [dbo].[ClientAddresses] (
        [ClientAddressId] BIGINT NOT NULL IDENTITY,
        [ClientId] BIGINT NOT NULL,
        [AddressLine1] NVARCHAR(128) NOT NULL,
        [AddressLine2] NVARCHAR(128) NULL DEFAULT (NULL),
        [AddressLine3] NVARCHAR(128) NULL DEFAULT (NULL),
        [AddressLine4] NVARCHAR(128) NULL DEFAULT (NULL),
        [City] NVARCHAR(96) NOT NULL,
        [Region] NVARCHAR(96) NULL DEFAULT (NULL),
        [PostalCode] NVARCHAR(8) NOT NULL,
        [CountryCode] NCHAR(2) NOT NULL,
        [IsActive] BIT NOT NULL,
        [IsPrimary] BIT NOT NULL,
        [UtcCreatedDate] DATETIME2 NOT NULL,
        [UtcUpdatedDate] DATETIME2 NULL DEFAULT (NULL),
        [UtcDeletedDate] DATETIME2 NULL DEFAULT (NULL),
        CONSTRAINT [PK_Addresses] PRIMARY KEY ([ClientAddressId]),
        CONSTRAINT [FK_ClientAddresses_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([ClientId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720040448_Init_Database')
BEGIN
    CREATE TABLE [dbo].[Invoices] (
        [InvoiceId] BIGINT NOT NULL IDENTITY,
        [Guid] UNIQUEIDENTIFIER NOT NULL DEFAULT (NEWID()),
        [ClientId] BIGINT NOT NULL,
        [Number] NVARCHAR(8) NOT NULL,
        [Description] NVARCHAR(128) NOT NULL,
        [UtcDate] DATE NOT NULL,
        [UtcDueDate] DATE NOT NULL,
        [Status] SMALLINT NOT NULL,
        [PaymentTermDays] SMALLINT NOT NULL,
        [UtcCreatedDate] DATETIME2 NOT NULL,
        [UtcUpdatedDate] DATETIME2 NULL DEFAULT (NULL),
        [UtcDeletedDate] DATETIME2 NULL DEFAULT (NULL),
        CONSTRAINT [PK_Invoices] PRIMARY KEY ([InvoiceId]),
        CONSTRAINT [FK_Invoices_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([ClientId]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720040448_Init_Database')
BEGIN
    CREATE TABLE [dbo].[InvoiceItems] (
        [InvoiceItemId] BIGINT NOT NULL IDENTITY,
        [InvoiceId] BIGINT NOT NULL,
        [Description] NVARCHAR(128) NOT NULL,
        [Quantity] INT NOT NULL,
        [Price] DECIMAL(19,4) NOT NULL,
        [UtcCreatedDate] DATETIME2 NOT NULL,
        [UtcUpdatedDate] DATETIME2 NULL DEFAULT (NULL),
        [UtcDeletedDate] DATETIME2 NULL DEFAULT (NULL),
        CONSTRAINT [PK_InvoiceItem] PRIMARY KEY ([InvoiceItemId]),
        CONSTRAINT [FK_InvoiceItem_Invoice_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoices] ([InvoiceId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720040448_Init_Database')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Addresses_City] ON [dbo].[ClientAddresses] ([City]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720040448_Init_Database')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Addresses_CountryCode] ON [dbo].[ClientAddresses] ([CountryCode]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720040448_Init_Database')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Addresses_Region] ON [dbo].[ClientAddresses] ([Region]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720040448_Init_Database')
BEGIN
    EXEC(N'CREATE UNIQUE NONCLUSTERED INDEX [UX_ClientAddresses_Address] ON [dbo].[ClientAddresses] ([ClientId], [AddressLine1], [AddressLine2], [AddressLine3], [AddressLine4], [City], [Region], [PostalCode], [CountryCode]) WHERE [AddressLine2] IS NOT NULL AND [AddressLine3] IS NOT NULL AND [AddressLine4] IS NOT NULL AND [Region] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720040448_Init_Database')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UX_Client_Name] ON [dbo].[Clients] ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720040448_Init_Database')
BEGIN
    CREATE INDEX [IX_InvoiceItems_InvoiceId] ON [dbo].[InvoiceItems] ([InvoiceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720040448_Init_Database')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Invoices_UtcDate] ON [dbo].[Invoices] ([UtcDate]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720040448_Init_Database')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Invoices_UtcDueDate] ON [dbo].[Invoices] ([UtcDueDate]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720040448_Init_Database')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UX_Invoices_ClientId_Number] ON [dbo].[Invoices] ([ClientId], [Number]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230720040448_Init_Database')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230720040448_Init_Database', N'6.0.20');
END;
GO

COMMIT;
GO

