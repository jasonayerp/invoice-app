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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230723193607_Init_Database')
BEGIN
    CREATE TABLE [dbo].[Clients] (
        [Id] BIGINT NOT NULL IDENTITY,
        [Guid] UNIQUEIDENTIFIER NOT NULL DEFAULT (NEWID()),
        [Name] NVARCHAR(56) NOT NULL,
        [Email] NVARCHAR(128) NULL DEFAULT (NULL),
        [CreatedAt] DATETIMEOFFSET(7) NOT NULL,
        [UpdatedAt] DATETIMEOFFSET(7) NULL DEFAULT (NULL),
        [DeletedAt] DATETIMEOFFSET(7) NULL DEFAULT (NULL),
        CONSTRAINT [PK_Clients] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230723193607_Init_Database')
BEGIN
    CREATE TABLE [dbo].[ClientAddresses] (
        [ClientAddressId] BIGINT NOT NULL IDENTITY,
        [ClientId] BIGINT NOT NULL,
        [Line1] NVARCHAR(128) NOT NULL,
        [Line2] NVARCHAR(128) NULL DEFAULT (NULL),
        [Line3] NVARCHAR(128) NULL DEFAULT (NULL),
        [Line4] NVARCHAR(128) NULL DEFAULT (NULL),
        [City] NVARCHAR(96) NOT NULL,
        [Region] NVARCHAR(96) NULL DEFAULT (NULL),
        [PostalCode] NVARCHAR(8) NOT NULL,
        [CountryCode] NCHAR(2) NOT NULL,
        [IsDefault] BIT NOT NULL,
        [CreatedAt] DATETIMEOFFSET(7) NOT NULL,
        [UpdatedAt] DATETIMEOFFSET(7) NULL DEFAULT (NULL),
        [DeletedAt] DATETIMEOFFSET(7) NULL DEFAULT (NULL),
        CONSTRAINT [PK_Addresses] PRIMARY KEY ([ClientAddressId]),
        CONSTRAINT [FK_ClientAddresses_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230723193607_Init_Database')
BEGIN
    CREATE TABLE [dbo].[Invoices] (
        [InvoiceId] BIGINT NOT NULL IDENTITY,
        [Guid] UNIQUEIDENTIFIER NOT NULL DEFAULT (NEWID()),
        [ClientId] BIGINT NOT NULL,
        [Number] NVARCHAR(8) NOT NULL,
        [Description] NVARCHAR(128) NOT NULL,
        [Date] DATE NOT NULL,
        [DueDate] DATE NOT NULL,
        [Status] SMALLINT(1) NOT NULL,
        [PaymentTermDays] SMALLINT(2) NOT NULL,
        [CreatedAt] DATETIMEOFFSET(7) NOT NULL,
        [UpdatedAt] DATETIMEOFFSET(7) NULL DEFAULT (NULL),
        [DeletedAt] DATETIMEOFFSET(7) NULL DEFAULT (NULL),
        CONSTRAINT [PK_Invoices] PRIMARY KEY ([InvoiceId]),
        CONSTRAINT [FK_Invoices_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230723193607_Init_Database')
BEGIN
    CREATE TABLE [dbo].[InvoiceItems] (
        [InvoiceItemId] BIGINT NOT NULL IDENTITY,
        [InvoiceId] BIGINT NOT NULL,
        [Description] NVARCHAR(128) NOT NULL,
        [Quantity] INT NOT NULL,
        [UnitPrice] DECIMAL(19,4) NOT NULL,
        [TotalPrice] AS ([Quantity] * [UnitPrice]),
        [CreatedAt] DATETIMEOFFSET(7) NOT NULL,
        [UpdatedAt] DATETIMEOFFSET(7) NULL DEFAULT (NULL),
        [DeletedAt] DATETIMEOFFSET(7) NULL DEFAULT (NULL),
        CONSTRAINT [PK_InvoiceItems] PRIMARY KEY ([InvoiceItemId]),
        CONSTRAINT [FK_InvoiceItems_Invoices_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoices] ([InvoiceId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230723193607_Init_Database')
BEGIN
    EXEC(N'CREATE UNIQUE NONCLUSTERED INDEX [UX_ClientAddresses_Address] ON [dbo].[ClientAddresses] ([ClientId], [Line1], [Line2], [Line3], [Line4], [City], [Region], [PostalCode], [CountryCode]) WHERE [Line2] IS NOT NULL AND [Line3] IS NOT NULL AND [Line4] IS NOT NULL AND [Region] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230723193607_Init_Database')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UX_ClientAddresses_DefaultAddress] ON [dbo].[ClientAddresses] ([ClientId], [IsDefault]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230723193607_Init_Database')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UX_Client_Name] ON [dbo].[Clients] ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230723193607_Init_Database')
BEGIN
    CREATE INDEX [IX_InvoiceItems_InvoiceId] ON [dbo].[InvoiceItems] ([InvoiceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230723193607_Init_Database')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Invoices_Date] ON [dbo].[Invoices] ([Date]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230723193607_Init_Database')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Invoices_DueDate] ON [dbo].[Invoices] ([DueDate]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230723193607_Init_Database')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UX_Invoices_ClientId_Number] ON [dbo].[Invoices] ([ClientId], [Number]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230723193607_Init_Database')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230723193607_Init_Database', N'6.0.20');
END;
GO

COMMIT;
GO

