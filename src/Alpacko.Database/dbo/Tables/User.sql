CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(256) NOT NULL, 
    [Password] NVARCHAR(100) NOT NULL, 
    [Salt] NVARCHAR(50) NOT NULL, 
    [PostOfficeId] INT NULL, 
    [UserRoleId] INT NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [ModifiedDate] DATETIME2 NULL, 
    [DeletedDate] DATETIME2 NULL, 
    CONSTRAINT [FK_User_UserRole] FOREIGN KEY ([UserRoleId]) REFERENCES [UserRole]([Id]), 
    CONSTRAINT [FK_User_PostOffice] FOREIGN KEY ([PostOfficeId]) REFERENCES [PostOffice]([Id])
)
