CREATE TABLE dbo.RestaurantWorker
(
	RestaurantWorkerId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	Firstname varchar(100) NOT NULL,
	Lastname varchar(100) NOT NULL,
	Nick varchar(100) NULL,
	PositionId INT NOT NULL,
	RestaurantId UNIQUEIDENTIFIER NULL,
	UserId INT NULL,
	Active BIT NOT NULL DEFAULT 1,
	CONSTRAINT [FK_RestaurantWorker_AppUser_UserId]
		FOREIGN KEY (UserId) REFERENCES AppUser(UserId),
	CONSTRAINT [FK_RestaurantWorker_Restaurant_RestaurantId]
		FOREIGN KEY (RestaurantId) REFERENCES Restaurant(RestaurantId),
	CONSTRAINT [FK_RestaurantWorker_Position_PositionId]
		FOREIGN KEY (PositionId) REFERENCES Position(PositionId)
)
GO