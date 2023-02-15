CREATE PROCEDURE [idpc].[UpdateApi]
	@IdpName nvarchar(40),
	@Name nvarchar(40),
	@DisplayName nvarchar(40)
AS
	UPDATE [idpc].[Api] SET [DisplayName] = @displayName WHERE [IdpName] = @IdpName AND [Name] = @Name
	SELECT * FROM [idpc].[Api] t WHERE [IdpName] = @IdpName AND [Name] = @Name
RETURN 0
GO
