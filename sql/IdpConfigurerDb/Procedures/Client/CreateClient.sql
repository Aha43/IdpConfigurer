CREATE PROCEDURE [idpc].[CreateClient]
	@idpName nvarchar(40),
	@clientId nvarchar(40),
	@clientName nvarchar(40),
	@json nvarchar(max)
AS
	INSERT INTO [idpc].[Client] ([ClientId], [ClientName], [IdpName], [Json]) values (@clientId, @clientName, @idpName, @json)
	SELECT * FROM [idpc].[Client] t WHERE t.[ClientId] = @clientId AND t.[IdpName] = @idpName
RETURN 0
GO
