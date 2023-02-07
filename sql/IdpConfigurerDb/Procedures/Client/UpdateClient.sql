CREATE PROCEDURE [idpc].[UpdateClient]
	@idpName nvarchar(40),
	@clientId nvarchar(40),
	@clientName nvarchar(40),
	@json nvarchar(40)
AS
	UPDATE [idpc].[Client] SET [ClientName] = @clientName, [Json] = @json WHERE ClientId = @clientId AND IdpName = @idpName 
RETURN 0
GO
