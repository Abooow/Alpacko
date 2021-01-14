CREATE TABLE [dbo].[RegisteredPackage]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PackageId] VARCHAR(10) NOT NULL, 
    [PostOfficeId] INT NOT NULL, 
    [RegisteredDate] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_RegisteredPackage_Package] FOREIGN KEY ([PackageId]) REFERENCES [Package]([Id]), 
    CONSTRAINT [FK_RegisteredPackage_PostOffice] FOREIGN KEY ([PostOfficeId]) REFERENCES [PostOffice]([Id])
)
