IF OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [dbo].[__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
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

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
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

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
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

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
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

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Addresses_City] ON [dbo].[Addresses] ([City]);
END;
GO

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Addresses_CountryCode] ON [dbo].[Addresses] ([CountryCode]);
END;
GO

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Addresses_Region] ON [dbo].[Addresses] ([Region]);
END;
GO

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UX_Addresses_Address] ON [dbo].[Addresses] ([AddressLine1], [City], [Region], [PostalCode], [CountryCode]);
END;
GO

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Client_Guid] ON [dbo].[Clients] ([Guid]);
END;
GO

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UX_Client_Name] ON [dbo].[Clients] ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE INDEX [IX_InvoiceItems_InvoiceId] ON [dbo].[InvoiceItems] ([InvoiceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UX_InvoiceItems_Description] ON [dbo].[InvoiceItems] ([Description]);
END;
GO

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE UNIQUE INDEX [IX_Invoices_BillFromAddressId] ON [dbo].[Invoices] ([BillFromAddressId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE UNIQUE INDEX [IX_Invoices_BillToAddressId] ON [dbo].[Invoices] ([BillToAddressId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE INDEX [IX_Invoices_ClientId] ON [dbo].[Invoices] ([ClientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UX_Invoices_Number] ON [dbo].[Invoices] ([Number]);
END;
GO

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230706052710_InitialMigration')
BEGIN
    INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230706052710_InitialMigration', N'6.0.19');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230710021819_AddViewInvoiceSummary')
BEGIN
    CREATE VIEW [vw_InvoiceSummary] AS
    SELECT
    		[I].[Id]
    	,	[I].[Guid]
    	,	[I].[Number]
    	,	[I].[UtcDate]
    	,	[I].[Status]
    	,	[I].[PaymentTerm]
    	,	[I].[UtcCreatedDate]
    	,	[I].[UtcUpdatedDate]
    	,	[I].[UtcDeletedDate]
    	,	SUM([II].[Amount]) AS [InvoiceItemsTotalAmount]
    	,	COUNT([II].[InvoiceId]) AS [InvoiceItemsCount]
    	,	[A].[AddressId] AS [BillFromAddressId]
    	,	[A].[AddressLine1] AS [BillFromAddressLine1]
    	,	[A].[AddressLine2] AS [BillFromAddressLine2]
    	,	[A].[AddressLine3] AS [BillFromAddressLine3]
    	,	[A].[AddressLine4] AS [BillFromAddressLin4]
    	,	[A].[City] AS [BillFromCity]
    	,	[A].[Region] AS [BillFromRegion]
    	,	[A].[PostalCode] AS [BillFromPostalCode]
    	,	[A].[CountryCode] AS [BillFromCountryCode]
    	,	[A1].[AddressId] AS [BillToAddressId]
    	,	[A1].[AddressLine1] AS [BillToAddressLine1]
    	,	[A1].[AddressLine2] AS [BillToAddressLine2]
    	,	[A1].[AddressLine3] AS [BillToAddressLine3]
    	,	[A1].[AddressLine4] AS [BillToAddressLine4]
    	,	[A1].[City] AS [BillToAddressCity]
    	,	[A1].[Region] AS [BillToRegion]
    	,	[A1].[PostalCode] AS [BillToPostalCode]
    	,	[A1].[CountryCode] AS [BillToCountryCode]
    	,	[C].[ClientId]
    	,	[C].[ClientName]
    	,	[C].[ClientPhone]
    	,	[C].[ClientEmail]
    FROM [dbo].[Invoices] AS [I]
    LEFT JOIN [dbo].[InvoiceItems] AS [II]
    	ON	[I].[InvoiceId] = [II].[InvoiceId]
    INNER JOIN [dbo].[Addresses] AS [A]
    	ON	[I].[BillFromAddressId] = [A].[AddressId]
    INNER JOIN [dbo].[Addresses] AS [A1]
    	ON	[I].[BillToAddressId] = [A1].[AddressId]
    INNER JOIN [dbo].[Clients] AS [C]
    	ON	[I].[ClientId] = [C].[ClientId]
    GROUP BY
    		[I].[InvoiceId]
    	,	[I].[Guid]
    	,	[I].[Number]
    	,	[I].[UtcDate]
    	,	[I].[Status]
    	,	[I].[PaymentTerm]
    	,	[I].[BillFromAddressId]
    	,	[I].[BillToAddressId]
    	,	[I].[ClientId]
    	,	[I].[UtcCreatedDate]
    	,	[I].[UtcUpdatedDate]
    	,	[I].[UtcDeletedDate]
    	,	[A].[AddressId]
    	,	[A].[AddressLine1]
    	,	[A].[AddressLine2]
    	,	[A].[AddressLine3]
    	,	[A].[AddressLine4]
    	,	[A].[City]
    	,	[A].[Region]
    	,	[A].[PostalCode]
    	,	[A].[CountryCode]
    	,	[A1].[AddressId]
    	,	[A1].[AddressLine1]
    	,	[A1].[AddressLine2]
    	,	[A1].[AddressLine3]
    	,	[A1].[AddressLine4]
    	,	[A1].[City]
    	,	[A1].[Region]
    	,	[A1].[PostalCode]
    	,	[A1].[CountryCode]
    	,	[C].[ClientId]
    	,	[C].[Name]
    	,	[C].[Phone]
    	,	[C].[Email];
END;
GO

IF NOT EXISTS(SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20230710021819_AddViewInvoiceSummary')
BEGIN
    INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230710021819_AddViewInvoiceSummary', N'6.0.19');
END;
GO

COMMIT;
GO

