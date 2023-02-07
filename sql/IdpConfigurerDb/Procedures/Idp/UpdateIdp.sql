CREATE PROCEDURE [idpc].[UpdateIdp]
	@name nvarchar(40),
	@uri nvarchar(256),
	@json nvarchar(max)
AS
	UPDATE [idpc].[Idp] SET [Uri] = @uri, [Json] = @json WHERE [Name] = @name 
RETURN 0
GO
