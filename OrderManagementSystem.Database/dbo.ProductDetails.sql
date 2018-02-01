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