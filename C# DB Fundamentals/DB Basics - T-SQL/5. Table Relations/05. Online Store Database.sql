CREATE TABLE Cities (
	CityID int PRIMARY KEY IDENTITY,
	Name nvarchar(50) NOT NULL
)

CREATE TABLE Customers (
	CustomerID int PRIMARY KEY IDENTITY,
	Name nvarchar(50) NOT NULL,
	Birthday date,
	CityID int NOT NULL,

	CONSTRAINT FK_Customers_Cities FOREIGN KEY(CityID) REFERENCES Cities(CityID)
)

CREATE TABLE Orders (
	OrderID int PRIMARY KEY IDENTITY,
	CustomerID int NOT NULL,
	
	CONSTRAINT FK_Orders_Customers FOREIGN KEY(CustomerID) REFERENCES Customers(CustomerID)		
)

CREATE TABLE ItemTypes (
	ItemTypeID int PRIMARY KEY IDENTITY,
	Name nvarchar(50)
)

CREATE TABLE Items (
	ItemID int PRIMARY KEY IDENTITY,
	Name nvarchar(50),
	ItemTypeID int NOT NULL,
	
	CONSTRAINT FK_Items_ItemTypes FOREIGN KEY(ItemTypeID) REFERENCES ItemTypes(ItemTypeID)
)

CREATE TABLE OrderItems (
	OrderID int,
	ItemID int

	CONSTRAINT PK_OrderItems PRIMARY KEY(OrderID, ItemID),
	CONSTRAINT FK_OrderItems_Orders FOREIGN KEY(OrderID) REFERENCES Orders(OrderID),
	CONSTRAINT FK_OrderItems_Items FOREIGN KEY(ItemID) REFERENCES Items(ItemID)
)