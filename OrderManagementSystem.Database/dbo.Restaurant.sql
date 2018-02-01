CREATE TABLE dbo.Restaurant
(
	RestaurantId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	Name varchar(255) NOT NULL,
	UniqueCode varchar(100) NOT NULL,
	City varchar(255) NOT NULL,	
	Street varchar(255) NOT NULL,
	PostalCode varchar(10) NOT NULL,
	StreetNumber int NOT NULL,
	FlatNumber int NULL,
	ManagerId UNIQUEIDENTIFIER NULL,
	PhotoUrl varchar(1000) NULL
)
GO