CREATE TABLE dbo.ProductCategory
(
	ProductCategoryId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	Code varchar(50) NOT NULL,
	Name varchar(255) NOT NULL,
	RestaurantId UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [FK_ProductCategory_Restaurant_RestaurantId] 
		FOREIGN KEY (RestaurantId) REFERENCES Restaurant(RestaurantId)
)
GO