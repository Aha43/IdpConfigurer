CREATE TABLE [idpc].[Idp]
(
	[Name] nvarchar(40) NOT NULL PRIMARY KEY,
	[Uri] nvarchar(256) NOT NULL,
	[Json] nvarchar(max) NULL
)
