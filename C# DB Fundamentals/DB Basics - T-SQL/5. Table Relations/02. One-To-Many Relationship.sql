CREATE TABLE Manufacturers (
  ManufacturerID int PRIMARY KEY NOT NULL,
  Name nvarchar(100) NOT NULL,
  EstablishedOn date
)

CREATE TABLE Models (
  ModelID int PRIMARY KEY NOT NULL,
  Name nvarchar(100) NOT NULL,
  ManufacturerID int NOT NULL,
  CONSTRAINT FK_Models_Manufacturers FOREIGN KEY (ManufacturerID)
  REFERENCES Manufacturers(ManufacturerID)
)

INSERT INTO Manufacturers VALUES
  (1, 'BMW', '1916/03/07'),
  (2, 'Tesla', '2003/01/01'),
  (3, 'Lada', '1966/05/01')

INSERT INTO Models VALUES
  (101, 'X1', 1),
  (102, 'i6', 1),
  (103, 'Model S', 2),
  (104, 'Model X', 2),
  (105, 'Model 3', 2),
  (106, 'Nova', 3)