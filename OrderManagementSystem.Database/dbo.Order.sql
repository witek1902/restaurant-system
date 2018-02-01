CREATE TABLE dbo.[Order]
(
	OrderId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	OrderCreationDate DATETIME NOT NULL,
	OrderFinishedDate DATETIME NULL,
	OrderStatusId INT NOT NULL,
	TableNumber INT NULL,
	Rate INT NULL,
	RateDetails varchar(1000) NULL,
	Comments varchar(1000) NULL,
	CookId UNIQUEIDENTIFIER NULL,
	CustomerId UNIQUEIDENTIFIER NOT NULL,
	WaiterId UNIQUEIDENTIFIER NULL,
	CONSTRAINT [FK_Order_RestaurantWorker_WaiterId]
		FOREIGN KEY (WaiterId) REFERENCES RestaurantWorker(RestaurantWorkerId),
	CONSTRAINT [FK_Order_RestaurantWorker_CookId]
		FOREIGN KEY (CookId) REFERENCES RestaurantWorker(RestaurantWorkerId),
	CONSTRAINT [FK_Order_Customer_CustomerId]
		FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId),
	CONSTRAINT [FK_Order_OrderStatus_OrderStatusId]
		FOREIGN KEY (OrderStatusId) REFERENCES OrderStatus(OrderStatusId)
)
GO