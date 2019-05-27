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