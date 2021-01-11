CREATE TABLE [dbo].[PostOffice]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Address] NVARCHAR(50) NOT NULL, 
    [ZipCode] NVARCHAR(50) NOT NULL, 
    [City] NVARCHAR(50) NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [ModifiedDate] DATETIME2 NULL, 
    [DeletedDate] DATETIME2 NULL
)
