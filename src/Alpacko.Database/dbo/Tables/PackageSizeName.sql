CREATE TABLE [dbo].[PackageSizeName]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] INT NOT NULL, 
    [Description] VARCHAR(50) NOT NULL,
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [ModifiedDate] DATETIME2 NULL, 
    [DeletedDate] DATETIME2
)
