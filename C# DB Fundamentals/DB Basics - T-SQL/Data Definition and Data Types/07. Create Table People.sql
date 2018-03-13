CREATE TABLE People 
(
	Id int PRIMARY KEY IDENTITY CHECK (Id < 2000000000),
	Name nvarchar(200) NOT NULL,
	Picture varbinary(2048),
	Height decimal(3,2),
	Weight decimal(4,2),
	Gender varchar(1) CHECK (Gender = 'f' OR Gender = 'm') NOT NULL,
	Birthdate date NOT NULL,
	Biography nvarchar(MAX)
)

Set DateFormat DMY
INSERT People (Name, Height, Weight, Gender, Birthdate)
VALUES 
('Ivan', 1.75, 67.12, 'm', '12/03/1972'), 
('Pesho', 1.60, 70, 'm', '13/02/1999'), 
('Maria', 1.72, 54, 'f', '22/06/1953'), 
('Gosho', 1.9, 85, 'm', '11/04/1983'), 
('Pencho', 1.50, 74, 'm', '02/02/2002')