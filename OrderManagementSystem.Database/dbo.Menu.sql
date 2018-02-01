CREATE TABLE dbo.Menu
(
	MenuId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	Code varchar(50) NOT NULL,
	Name varchar(255) NOT NULL,
	RestaurantId UNIQUEIDENTIFIER NOT NULL,
	Active BIT NOT NULL DEFAULT 1,
	CONSTRAINT [FK_Menu_Restaurant_RestaurantId] 
		FOREIGN KEY (RestaurantId) REFERENCES Restaurant(RestaurantId)
)
GO

CREATE INDEX IX_RestaurantMenu_RestaurantId ON dbo.Menu (RestaurantId)
GO