CREATE TABLE Directors
(
	Id int PRIMARY KEY IDENTITY,
	DirectorName varchar(50) NOT NULL,
	Notes varchar(max)
)

CREATE TABLE Genres
(
	Id int PRIMARY KEY IDENTITY,
	GenreName varchar(50) NOT NULL,
	Notes varchar(max)
)

CREATE TABLE Categories
(
	Id int PRIMARY KEY IDENTITY,
	CategoryName varchar(50) NOT NULL,
	Notes varchar(max)
)

CREATE TABLE Movies
(
	Id int PRIMARY KEY IDENTITY,
	Title varchar(50) NOT NULL,
	DirectorId int NOT NULL,
	CopyRightYear date NOT NULL,
	Length int,
	GenreId int NOT NULL,
	CategoryId int NOT NULL,
	Rating int,
	Notes varchar(max)
)

INSERT Directors (DirectorName)
VALUES ('Pesho Peshev'), ('Gosho Goshev'), ('Ivan Ivanov'), ('Marin Marinkin'), ('Naiden Naidenov')

INSERT Genres (GenreName)
VALUES ('Comedy'), ('Drama'), ('Horror'), ('Action'), ('Thriller')

INSERT Categories (CategoryName)
VALUES ('Family-Friendly'), ('Mature-Content'), ('Blood And Gore'), ('18+'), ('Animation')

Set DateFormat DMY
INSERT Movies (Title, DirectorId, CopyRightYear, GenreId, CategoryId)
VALUES
	('Titanic', 1, '12/11/1825', 2, 2),
	('Stardust', 2, '05/10/2001', 1, 1),
	('Dead Silence', 3, '12/12/2012', 3, 2),
	('Tango & Cash', 4, '05/02/1994', 4, 4),
	('Steins;Gate', 5, '01/01/2001', 5, 5)