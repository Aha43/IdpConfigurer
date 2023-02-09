CREATE PROCEDURE [idpc].[UpdateIdp]
	@name nvarchar(40),
	@newName nvarchar(40),
	@uri nvarchar(256),
	@json nvarchar(max)
AS
	if (@newName is null) begin
		UPDATE [idpc].[Idp] SET [Uri] = @uri, [Json] = @json WHERE [Name] = @name
		SELECT * FROM [idpc].[Idp] t WHERE t.[Name] = @name
	end else begin
		UPDATE [idpc].[Idp] SET [Name] = @newName, [Uri] = @uri, [Json] = @json WHERE [Name] = @name
		SELECT * FROM [idpc].[Idp] t WHERE t.[Name] = @newName
	end
RETURN 0
GO
