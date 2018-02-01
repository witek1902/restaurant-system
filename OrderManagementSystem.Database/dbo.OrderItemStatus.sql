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