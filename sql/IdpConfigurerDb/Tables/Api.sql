CREATE TABLE [idpc].[Api]
(
	[Name] nvarchar(40) NOT NULL,
	[IdpName] nvarchar(40) NOT NULL,
	[DisplayName] nvarchar(40) NOT NULL,

	CONSTRAINT FK_Api_Idp FOREIGN KEY (IdpName)
	REFERENCES [idpc].[Idp](Name)
)
GO

ALTER TABLE [idpc].[Api]
ADD CONSTRAINT PK_Api PRIMARY KEY (Name, IdpName)
GO
