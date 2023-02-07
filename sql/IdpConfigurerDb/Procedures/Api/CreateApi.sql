CREATE PROCEDURE [idpc].[CreateApi]
	@ipdName nvarchar(40),
	@Name nvarchar(40),
	@displayName nvarchar(40)
AS
	INSERT INTO [idpc].[Api] ([IdpName], [Name], [DisplayName]) values (@ipdName, @Name, @displayName)
RETURN 0
GO
