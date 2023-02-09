CREATE PROCEDURE [idpc].[UpdateClient]
	@idpName nvarchar(40),
	@clientId nvarchar(40),
	@clientName nvarchar(40),
	@json nvarchar(max)
AS
	UPDATE [idpc].[Client] SET [ClientName] = @clientName, [Json] = @json WHERE [ClientId] = @clientId AND [IdpName] = @idpName 
	SELECT * FROM [idpc].[Client] t WHERE t.[ClientId] = @clientId AND t.[IdpName] = @idpName
RETURN 0
GO
