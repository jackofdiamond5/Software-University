CREATE TABLE Users
(
	Id int PRIMARY KEY IDENTITY CHECK (Id < 2000000000),
	Username nvarchar(30) UNIQUE NOT NULL,
	Password nvarchar(26) NOT NULL,
	ProfilePicture varbinary(MAX),
	LastLoginTime datetime,
	IsDeleted bit  DEFAULT 0
)

INSERT Users (UserName, Password)
VALUES
	('ivan_ivan', 'dada354321_31'),
	('pesho_peshev', 'pesho_1321@'),
	('gosho.goshev', 'goshkata159.31@/31'),
	('peca7a', '21123123123@@@'),
	('gencho', 'ocolgul123')