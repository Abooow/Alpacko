CREATE TABLE [dbo].[PackageRecipient]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Co] NVARCHAR(50) NULL, 
    [Address] NVARCHAR(50) NOT NULL, 
    [ZipCode] NVARCHAR(5) NOT NULL, 
    [City] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(50) NULL, 
    [PhoneNumber] NVARCHAR(50) NULL
)
