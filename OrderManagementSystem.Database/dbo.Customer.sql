CREATE TABLE dbo.Customer
(
	CustomerId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	Firstname varchar(100) NOT NULL,
	UserId INT NOT NULL,
	CONSTRAINT [FK_Customer_AppUser_UserId]
		FOREIGN KEY (UserId) REFERENCES AppUser(UserId)
)