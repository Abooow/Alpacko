CREATE TABLE [dbo].[SentPackage]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NULL, 
    [PackageId] VARCHAR(10) NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    CONSTRAINT [FK_SentPackage_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [FK_SentPackage_Package] FOREIGN KEY ([PackageId]) REFERENCES [Package]([Id])
)
