CREATE TABLE [idpc].[Client]
(
	[ClientId] nvarchar(40) NOT NULL,
	[ClientName] nvarchar(40) NOT NULL,
	[IdpName] nvarchar(40) NOT NULL,
	[Json] nvarchar NOT NULL,

	CONSTRAINT FK_Client_Idp FOREIGN KEY (IdpName)
	REFERENCES [idpc].Idp(Name)
)
GO

ALTER TABLE [idpc].[Client]
ADD CONSTRAINT PK_Client PRIMARY KEY (ClientId, IdpName)
GO
