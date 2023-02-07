CREATE PROCEDURE [idpc].[DeleteIdp]
	@name nvarchar(40)
AS
	DELETE [idpc].[Idp] WHERE [Name] = @name
RETURN 0
GO
