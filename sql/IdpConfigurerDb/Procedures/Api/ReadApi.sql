CREATE PROCEDURE [idpc].[ReadApi]
	@IdpName nvarchar(40),
	@Name nvarchar(40)
AS
	SELECT * FROM [idpc].[Api] t WHERE t.[IdpName] = @IdpName AND t.[Name] = @IdpName
RETURN 0
