use OMSDb

CREATE TABLE dbo.AppUser
(
	UserId INT NOT NULL IDENTITY PRIMARY KEY,
	[Login] varchar(255) NOT NULL
)

INSERT INTO dbo.AppUser
VALUES
('rw-manager'),
('rf-manager'),
('rt-manager'),
('rs-manager'),
('rw-waiter'),
('rf-waiter'),
('rt-waiter'),
('rs-waiter'),
('rw-Cook'),
('rf-Cook'),
('rt-Cook'),
('rs-Cook'),
('customer')

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

CREATE TABLE dbo.Product
(
	ProductId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	MenuId UNIQUEIDENTIFIER NOT NULL,
	Name varchar(255) NOT NULL,
	ProductCategoryId UNIQUEIDENTIFIER NOT NULL,
	[Description] varchar(1000) NOT NULL,
	Price DECIMAL(19,2) NOT NULL,
	PercentDiscount INT NULL,
	ProductDetailsId UNIQUEIDENTIFIER null,
	PhotoUrl varchar(2000) null,
	Active bit not null default 1,
	CONSTRAINT [FK_Product_ProductCategory_ProductCategoryId]
		FOREIGN KEY (ProductCategoryId) REFERENCES ProductCategory(ProductCategoryId),
	CONSTRAINT [FK_Product_Menu_MenuId]
		FOREIGN KEY (MenuId) REFERENCES Menu(MenuId)
)
GO

CREATE INDEX IX_Product_RestaurantMenuId ON dbo.Product(MenuId)
GO
CREATE INDEX IX_Product_ProductCategoryId ON dbo.Product(ProductCategoryId)
GO

CREATE TABLE dbo.ProductDetails
(
	ProductDetailsId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	Calories INT NULL,
	Protein INT NULL,
	Carbohydrates INT NULL,
	Fat INT NULL
)
GO

ALTER TABLE dbo.Product
ADD
	CONSTRAINT [FK_Product_ProductDetails_ProductDetailsId]
		FOREIGN KEY (ProductDetailsId) REFERENCES ProductDetails(ProductDetailsId)
GO

CREATE TABLE dbo.OrderStatus
(
	OrderStatusId INT NOT NULL PRIMARY KEY,
	Name varchar(100) NOT NULL,
	Code varchar(3) NOT NULL
)
GO

INSERT INTO dbo.OrderStatus
(OrderStatusId, Name, Code)
VALUES
(1,'Otwarte', 'OT'),
(2,'Przypisane do kelnera', 'PK'),
(3,'Zamknięte', 'ZA'),
(4,'Opłacone', 'OP'),
(5,'Odrzucone', 'OD')


CREATE TABLE dbo.Position
(
	PositionId INT NOT NULL PRIMARY KEY,
	Name varchar(100) not null,
	Code varchar(5) not null
)

INSERT INTO dbo.Position
(PositionId, Name, Code)
VALUES
(1, 'Waiter', 'KE'),
(2, 'Cook', 'KU'),
(3, 'Manager', 'MA')

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

CREATE TABLE dbo.Customer
(
	CustomerId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	Firstname varchar(100) NOT NULL,
	UserId INT NOT NULL,
	CONSTRAINT [FK_Customer_AppUser_UserId]
		FOREIGN KEY (UserId) REFERENCES AppUser(UserId)
)

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

CREATE TABLE dbo.OrderItemStatus
(
	OrderItemStatusId INT NOT NULL PRIMARY KEY,
	Name varchar(100) NOT NULL,
	Code varchar(3) NOT NULL
)

INSERT INTO dbo.OrderItemStatus
(OrderItemStatusId, Name, Code)
VALUES
(1, 'Nowa', 'NO'),
(2, 'Zatwierdzona', 'ZA'),
(3, 'W przygotowaniu w kuchni', 'PR'),
(4, 'Gotowe', 'GO'),
(5, 'Dostarczone', 'DO')

CREATE TABLE dbo.OrderItem
(
	OrderItemId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	Quantity int not null,
	ProductId UNIQUEIDENTIFIER NOT NULL,
	OrderId UNIQUEIDENTIFIER NOT NULL,
	OrderItemStatusId INT NOT NULL,
	CONSTRAINT [FK_OrderItem_OrderItemStatus_OrderItemStatusId]
		FOREIGN KEY (OrderItemStatusId) REFERENCES OrderItemStatus(OrderItemStatusId),
	CONSTRAINT [FK_OrderItem_Order_OrderId]
		FOREIGN KEY (OrderId) REFERENCES [Order](OrderId),
	CONSTRAINT [FK_OrderItem_Product_ProductId]
		FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
)