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