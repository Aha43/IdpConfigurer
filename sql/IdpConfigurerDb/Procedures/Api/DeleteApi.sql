CREATE PROCEDURE [idpc].[DeleteApi]
	@IdpName nvarchar(40),
	@Name nvarchar(40)
AS
	DELETE [idpc].[Api] WHERE [IdpName] = @IdpName AND [Name] = @Name
RETURN 0
GO
