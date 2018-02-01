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