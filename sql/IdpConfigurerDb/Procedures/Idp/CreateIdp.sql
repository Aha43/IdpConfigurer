CREATE PROCEDURE [idpc].[CreateIdp]
	@name nvarchar(40),
	@uri nvarchar(256)
AS
	INSERT INTO [idpc].[Idp] ([Name], [Uri]) values (@name, @uri)
RETURN 0
GO
