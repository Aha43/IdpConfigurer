CREATE PROCEDURE [idpc].[CreateIdp]
	@name nvarchar(40),
	@uri nvarchar(256),
	@json nvarchar(max)
AS
	INSERT INTO [idpc].[Idp] ([Name], [Uri], [Json]) values (@name, @uri, @json)
	SELECT * FROM [idpc].[Idp] t WHERE t.[Name] = @name
RETURN 0
GO
