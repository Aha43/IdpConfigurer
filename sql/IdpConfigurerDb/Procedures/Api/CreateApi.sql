CREATE PROCEDURE [idpc].[CreateApi]
	@ipdName nvarchar(40),
	@Name nvarchar(40),
	@displayName nvarchar(40)
AS
	INSERT INTO [idpc].[Api] ([IdpName], [Name], [DisplayName]) values (@ipdName, @Name, @displayName)
	SELECT * FROM [idpc].[Api] t WHERE t.[IdpName] = @ipdName AND t.[Name] = @Name
RETURN 0
GO
