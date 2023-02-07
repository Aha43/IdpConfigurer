CREATE PROCEDURE [idpc].[ReadApis]
	@IdpName nvarchar(40)
AS
	SELECT * FROM [idpc].[Api] p WHERE p.[IdpName] = @IdpName
RETURN 0
