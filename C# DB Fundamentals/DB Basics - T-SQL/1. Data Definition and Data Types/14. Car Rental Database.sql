CREATE TABLE Categories
(
	Id int PRIMARY KEY IDENTITY,
	CategoryName varchar(50) NOT NULL,
	DailyRate int,
	WeeklyRate int,
	MonthlyRate int,
	WeekendRate int
)

INSERT Categories(CategoryName)
VALUES
	('Pickup Truck'),
	('Sports Car'),
	('Retro Car')

CREATE TABLE Cars
(
	Id int PRIMARY KEY IDENTITY,
	PlateNumber varchar(50) NOT NULL,
	Manufacturer nvarchar(50) NOT NULL,
	Model nvarchar(30) NOT NULL,
	CarYear date,
	CategoryId int NOT NULL,
	Doors int,
	Picture varbinary(max),
	Condition varchar(30) NOT NULL,
	Available binary NOT NULL
)

INSERT Cars(PlateNumber, Manufacturer, Model, CategoryId, Condition, Available)
VALUES
	('LS13513FS', 'Pesho Industries', 'Peshevica', 1, 'Good', 1),
	('FF9874D13L', 'Gosho Industries', 'Goshoudi', 2, 'Excellent', 1),
	('N3310PH', 'Naiden Industries', 'NoKIA', 3, 'Amazing', 0)

CREATE TABLE Employees
(
	Id int PRIMARY KEY IDENTITY,
	FirstName nvarchar(30) NOT NULL,
	LastName nvarchar(30) NOT NULL,
	Title nvarchar(30) NOT NULL,
	Notes nvarchar(max)
)

INSERT Employees (FirstName, LastName, Title)
VALUES
	('Gencho', 'Petkov', 'Auto Mechanic'),
	('Marin', 'Ivanov', 'CEO'),
	('Ivaylo', 'Todorov', 'Car Engineer')

CREATE TABLE Customers
(
	Id int PRIMARY KEY IDENTITY,
	DriverLicenseNumber varchar(20) NOT NULL,
	FullName nvarchar(50) NOT NULL,
	Address nvarchar(50) NOT NULL,
	City nvarchar(50) NOT NULL,
	ZIPCode nvarchar(50),
	Notes nvarchar(max)
)

INSERT Customers (DriverLicenseNumber, FullName, Address, City)
VALUES
	('441235316124', 'Pencho Ivanov Gerov', 'Some Street', 'Sofia'),
	('222222222222', 'Stanimir Peshev Ivanov', 'Some Street', 'Plovdiv'),
	('123456789098', 'Nikola Naidenov Nikolov', 'Some Street', 'Varna')

CREATE TABLE RentalOrders
(
	Id int PRIMARY KEY IDENTITY,
	EmployeeId int NOT NULL,
	CustomerId int NOT NULL,
	CarId int NOT NULL,
	TankLevel int,
	KilometrageStart int,
	KilometrageEnd int,
	TotalKilometrage int NOT NULL,
	StartDate date,
	EndDate date,
	TotalDays int NOT NULL,
	RateApplied money NOT NULL,
	TaxRate money NOT NULL,
	OrderStatus nvarchar(50) NOT NULL,
	Notes nvarchar(max)
)

INSERT RentalOrders 
		(EmployeeId, CustomerId, CarId, TotalKilometrage, TotalDays, RateApplied, TaxRate, OrderStatus)
VALUES
	(1, 1, 1, 1800, 10, 0.85, 0.5, 'Confirmed'),
	(2, 2, 2, 2500, 15, 0.99, 1.25, 'Pending'),
	(3, 3, 3, 1200, 13, 0.53, 0.75, 'Declined')