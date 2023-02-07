CREATE PROCEDURE [idpc].[ReadClients]
	@IdpName nvarchar(40)
AS
	SELECT * FROM [idpc].[Client] t WHERE t.[IdpName] = @IdpName
RETURN 0
GO
