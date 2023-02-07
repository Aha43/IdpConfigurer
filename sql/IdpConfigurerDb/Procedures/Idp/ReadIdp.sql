CREATE PROCEDURE [idpc].[ReadIdp]
	@name nvarchar(40)
AS
	SELECT * FROM [idpc].[Idp] t WHERE t.[Name] = @name 
RETURN 0
GO
