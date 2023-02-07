CREATE PROCEDURE [idpc].[UpdateApi]
	@idpName nvarchar(40),
	@name nvarchar(40),
	@displayName nvarchar(40)
AS
	UPDATE [idpc].[Api] SET [DisplayName] = @displayName WHERE [IdpName] = @idpName AND [Name] = @name
RETURN 0
GO
