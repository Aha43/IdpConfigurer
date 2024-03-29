﻿CREATE TABLE [idpc].[Client]
(
	[ClientId] nvarchar(40) NOT NULL,
	[ClientName] nvarchar(40) NOT NULL,
	[IdpName] nvarchar(40) NOT NULL,
	[Json] nvarchar(max) NOT NULL,

	CONSTRAINT FK_Client_Idp FOREIGN KEY (IdpName)
	REFERENCES [idpc].Idp(Name)
	ON DELETE CASCADE
	ON UPDATE CASCADE
)
GO

ALTER TABLE [idpc].[Client]
ADD CONSTRAINT PK_Client PRIMARY KEY (ClientId, IdpName)
GO
