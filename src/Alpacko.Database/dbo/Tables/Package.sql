CREATE TABLE [dbo].[Package]
(
	[Id] VARCHAR(10) NOT NULL PRIMARY KEY, 
    [SenderId] INT NOT NULL, 
    [RecipientId] INT NOT NULL, 
    [PackageDetailId] INT NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    CONSTRAINT [FK_Package_PackageSender] FOREIGN KEY ([SenderId]) REFERENCES [PackageSender]([Id]), 
    CONSTRAINT [FK_Package_PackageRecipient] FOREIGN KEY ([RecipientId]) REFERENCES [PackageRecipient]([Id]), 
    CONSTRAINT [FK_Package_PackageDetail] FOREIGN KEY ([PackageDetailId]) REFERENCES [PackageDetail]([Id])
)
