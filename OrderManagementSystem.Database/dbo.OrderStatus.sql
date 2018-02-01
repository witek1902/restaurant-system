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