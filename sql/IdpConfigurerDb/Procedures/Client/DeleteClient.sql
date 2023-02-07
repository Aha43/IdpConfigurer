CREATE PROCEDURE [idpc].[DeleteClient]
	@idpName nvarchar(40),
	@clientId nvarchar(40)
AS
	DELETE FROM [idpc].[Client] WHERE [IdpName] = @idpName AND [ClientId] = @clientId
RETURN 0
GO
