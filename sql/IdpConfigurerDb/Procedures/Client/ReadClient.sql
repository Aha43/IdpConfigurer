CREATE PROCEDURE [idpc].[ReadClient]
	@IdpName nvarchar(40),
	@ClientId nvarchar(40)
AS
	SELECT * FROM [idpc].[Client] WHERE [IdpName] = @IdpName AND [ClientId] = @ClientId
RETURN 0
GO
