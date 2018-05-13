REATE TABLE Passports (
	PassportID int PRIMARY KEY,
	PassportNumber varchar(50)
)

CREATE TABLE Persons (
	PersonID int PRIMARY KEY IDENTITY,
	FirstName varchar(50),
	Salary money,
	PassportID int NOT NULL UNIQUE,
	CONSTRAINT FK_Persons_Passports FOREIGN KEY(PassportID) REFERENCES Passports(PassportID)
)

INSERT INTO Passports VALUES
(101, 'N34FG21B'),
(102, 'K65LO4R7'),
(103, 'ZE657QP2')

INSERT INTO Persons(FirstName,  Salary, PassportID) VALUES
('Roberto', 43300.00, 102),
('Tom', 56100.00, 103),
('Yana', 60200.00, 101)