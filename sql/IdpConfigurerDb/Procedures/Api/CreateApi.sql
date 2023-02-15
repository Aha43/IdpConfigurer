CREATE PROCEDURE [idpc].[CreateApi]
	@IdpName nvarchar(40),
	@Name nvarchar(40),
	@DisplayName nvarchar(40)
AS
	INSERT INTO [idpc].[Api] ([IdpName], [Name], [DisplayName]) values (@IdpName, @Name, @DisplayName)
	SELECT * FROM [idpc].[Api] t WHERE t.[IdpName] = @IdpName AND t.[Name] = @Name
RETURN 0
GO
