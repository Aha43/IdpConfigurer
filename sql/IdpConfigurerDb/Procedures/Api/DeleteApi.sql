CREATE PROCEDURE [idpc].[DeleteApi]
	@idpName nvarchar(40),
	@name nvarchar(40)
AS
	DELETE [idpc].[Api] WHERE [IdpName] = @idpName AND [Name] = @name
RETURN 0
GO
