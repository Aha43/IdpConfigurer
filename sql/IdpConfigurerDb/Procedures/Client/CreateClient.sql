CREATE PROCEDURE [idpc].[CreateClient]
	@idpName nvarchar(40),
	@clientId nvarchar(40),
	@clientName nvarchar(40)
AS
	INSERT INTO [idpc].[Client] (ClientId, ClientName, IdpName) values (@clientId, @clientName, @idpName)
RETURN 0
GO
