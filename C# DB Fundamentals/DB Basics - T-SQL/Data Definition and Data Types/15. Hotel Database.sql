CREATE TABLE Employees
(
	Id int PRIMARY KEY IDENTITY,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	Title nvarchar(50) NOT NULL,
	Notes nvarchar(max)
)

INSERT Employees (FirstName, LastName, Title)
VALUES
	('Pesho', 'Peshev', 'Janitor'), 
	('Gosho', 'Goshev', 'Security Manager'), 
	('Ivan', 'Ivanov', 'Hotel Manager')


CREATE TABLE Customers
(
	AccountNumber int PRIMARY KEY,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	PhoneNumber nvarchar(50) NOT NULL,
	EmergencyName nvarchar(50),
	EmergencyNumber nvarchar(50),
	Notes nvarchar(max)
)

INSERT Customers (AccountNumber, FirstName, LastName, PhoneNumber)
VALUES 
	(1258912,'Pencho', 'Dimitrov', '+3598012345678'),
	(1316478, 'Gergana', 'Ivanova', '+3487312305349'),
	(2245245, 'Vasil', 'Vasilov', '+3841424262433')


CREATE TABLE RoomStatus
(
	RoomStatus varchar(30) NOT NULL,
	Notes nvarchar(max)
)

INSERT RoomStatus (RoomStatus)
VALUES 
	('Occupied'), 
	('Free'), 
	('Getting It Ready')


CREATE TABLE RoomTypes
(
	RoomType varchar(50) NOT NULL,
	Notes nvarchar(max)
)

INSERT RoomTypes (RoomType) 
VALUES 
	('Small Room'), 
	('Large Room'), 
	('Kinsize Room')


CREATE TABLE BedTypes
(
	BedType varchar(50) NOT NULL,
	Notes nvarchar(max)
)

INSERT BedTypes (BedType)
VALUES
	('Small Bed'), 
	('Big Bed'), 
	('Trampoline')


CREATE TABLE Rooms
(
	RoomNumber int PRIMARY KEY,
	RoomType varchar(50) NOT NULL,
	BedType varchar(50) NOT NULL,
	Rate int,
	RoomStatus varchar(50) NOT NULL,
	Notes nvarchar(max)
)

INSERT Rooms (RoomNumber, RoomType, BedType, RoomStatus)
VALUES
	(420, 1, 1, 1), 
	(421, 2, 2, 2), 
	(422, 3, 3, 3)


CREATE TABLE Payments
(
	Id int PRIMARY KEY IDENTITY,
	EmployeeId int NOT NULL,
	PaymentDate date NOT NULL,
	AccountNumber int NOT NULL,
	FirstDateOccupied date,
	LastDateOccupied date,
	TotalDays int NOT NULL,
	AmountCharged money NOT NULL,
	TaxRate decimal,
	TaxAmount decimal,
	PaymentTotal money,
	Notes nvarchar(max)
)

Set DateFormat DMY
INSERT Payments (EmployeeId, PaymentDate, AccountNumber, TotalDays, AmountCharged)
VALUES
	(1, '11/11/2011', 123456, 11, 1111),
	(2, '10/10/2010', 654321, 10, 1010),
	(3, '09/09/2009', 999999, 99, 9999)


CREATE TABLE Occupancies
(
	Id int PRIMARY KEY IDENTITY,
	EmployeeId int NOT NULL,
	DateOccupied date NOT NULL,
	AccountNumber int NOT NULL,
	RoomNumber int NOT NULL,
	RateApplied decimal,
	PhoneCharge decimal,
	Notes nvarchar(max)
)

INSERT Occupancies (EmployeeId, DateOccupied, AccountNumber, RoomNumber)
VALUES
	(1, '01/01/2001', 111111, 1),
	(2, '02/02/2002', 222222, 2),
	(3, '03/03/2003', 333333, 3)