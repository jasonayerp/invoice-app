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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE TABLE [dbo].[Addresses] (
        [AddressId] BIGINT NOT NULL IDENTITY,
        [Guid] UNIQUEIDENTIFIER NOT NULL DEFAULT (NEWID()),
        [AddressLine1] NVARCHAR(128) NOT NULL,
        [AddressLine2] NVARCHAR(128) NULL DEFAULT (NULL),
        [AddressLine3] NVARCHAR(128) NULL DEFAULT (NULL),
        [AddressLine4] NVARCHAR(128) NULL DEFAULT (NULL),
        [City] NVARCHAR(128) NOT NULL,
        [Region] NVARCHAR(128) NOT NULL,
        [PostalCode] NVARCHAR(128) NOT NULL,
        [CountryCode] NCHAR(2) NOT NULL,
        [UtcCreatedDate] DATETIME2 NOT NULL,
        [UtcUpdatedDate] DATETIME2 NULL DEFAULT (NULL),
        [UtcDeletedDate] DATETIME2 NULL DEFAULT (NULL),
        CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED ([AddressId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE TABLE [dbo].[Clients] (
        [ClientId] BIGINT NOT NULL IDENTITY,
        [Guid] UNIQUEIDENTIFIER NOT NULL DEFAULT (NEWID()),
        [Name] NVARCHAR(50) NOT NULL,
        [Phone] NVARCHAR(50) NULL DEFAULT (NULL),
        [Email] NVARCHAR(50) NULL DEFAULT (NULL),
        [UtcCreatedDate] DATETIME2 NOT NULL,
        [UtcUpdatedDate] DATETIME2 NULL DEFAULT (NULL),
        [UtcDeletedDate] DATETIME2 NULL DEFAULT (NULL),
        CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED ([ClientId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE TABLE [dbo].[Invoices] (
        [InvoiceId] BIGINT NOT NULL IDENTITY,
        [Guid] UNIQUEIDENTIFIER NOT NULL DEFAULT (NEWID()),
        [Number] NVARCHAR(30) NOT NULL,
        [UtcDate] DATETIME2 NOT NULL,
        [Status] SMALLINT NOT NULL,
        [PaymentTerm] SMALLINT NOT NULL,
        [BillFromAddressId] BIGINT NOT NULL,
        [BillToAddressId] BIGINT NOT NULL,
        [ClientId] BIGINT NOT NULL,
        [UtcCreatedDate] DATETIME2 NOT NULL,
        [UtcUpdatedDate] DATETIME2 NULL DEFAULT (NULL),
        [UtcDeletedDate] DATETIME2 NULL DEFAULT (NULL),
        CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED ([InvoiceId]),
        CONSTRAINT [FK_Invoices_Addresses_BillFromAddressId] FOREIGN KEY ([BillFromAddressId]) REFERENCES [dbo].[Addresses] ([AddressId]),
        CONSTRAINT [FK_Invoices_Addresses_BillToAddressId] FOREIGN KEY ([BillToAddressId]) REFERENCES [dbo].[Addresses] ([AddressId]),
        CONSTRAINT [FK_Invoices_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([ClientId]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE TABLE [dbo].[InvoiceItems] (
        [InvoiceItemId] BIGINT NOT NULL IDENTITY,
        [InvoiceId] BIGINT NOT NULL,
        [Description] NVARCHAR(128) NOT NULL,
        [Quantity] INT NOT NULL,
        [Amount] DECIMAL(19,4) NOT NULL,
        [UtcCreatedDate] DATETIME2 NOT NULL,
        [UtcUpdatedDate] DATETIME2 NULL DEFAULT (NULL),
        [UtcDeletedDate] DATETIME2 NULL DEFAULT (NULL),
        CONSTRAINT [PK_InvoiceItem] PRIMARY KEY CLUSTERED ([InvoiceItemId]),
        CONSTRAINT [FK_InvoiceItem_Invoice_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoices] ([InvoiceId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Addresses_City] ON [dbo].[Addresses] ([City]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Addresses_CountryCode] ON [dbo].[Addresses] ([CountryCode]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Addresses_Region] ON [dbo].[Addresses] ([Region]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UX_Addresses_Address] ON [dbo].[Addresses] ([AddressLine1], [City], [Region], [PostalCode], [CountryCode]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Client_Guid] ON [dbo].[Clients] ([Guid]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UX_Client_Name] ON [dbo].[Clients] ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE INDEX [IX_InvoiceItems_InvoiceId] ON [dbo].[InvoiceItems] ([InvoiceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UX_InvoiceItems_Description] ON [dbo].[InvoiceItems] ([Description]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE UNIQUE INDEX [IX_Invoices_BillFromAddressId] ON [dbo].[Invoices] ([BillFromAddressId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE UNIQUE INDEX [IX_Invoices_BillToAddressId] ON [dbo].[Invoices] ([BillToAddressId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE INDEX [IX_Invoices_ClientId] ON [dbo].[Invoices] ([ClientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UX_Invoices_Number] ON [dbo].[Invoices] ([Number]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230706052710_InitialMigration', N'6.0.19');
END;
GO

COMMIT;
GO

