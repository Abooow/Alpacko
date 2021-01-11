CREATE TABLE [dbo].[PackageDetail]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [SizeNameId] INT NOT NULL, 
    [Price] MONEY NOT NULL, 
    [MinLenght] FLOAT NOT NULL, 
    [MaxLenght] FLOAT NOT NULL, 
    [MinWidth] FLOAT NOT NULL, 
    [MaxWidth] FLOAT NOT NULL, 
    [MinHeight] INT NULL, 
    [MaxHeight] INT NOT NULL, 
    [MinWeight] INT NULL, 
    [MaxWeight] INT NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [ModifiedDate] DATETIME2 NULL, 
    [DeletedDate] DATETIME2 NULL, 
    CONSTRAINT [FK_PackageDetail_PackageSize] FOREIGN KEY ([SizeNameId]) REFERENCES [PackageSizeName]([Id])
)
