CREATE TABLE Subjects (
	SubjectID int PRIMARY KEY IDENTITY,
	SubjectName nvarchar(30)
)

CREATE TABLE Majors (
	MajorID int PRIMARY KEY IDENTITY,
	Name nvarchar(30)
)

CREATE TABLE Students (
	StudentID int PRIMARY KEY IDENTITY,
	StudnetNumber int NOT NULL,
	StudentName nvarchar(30),
	MajorID int NOT NULL,

	CONSTRAINT FK_Students_Majors FOREIGN KEY(MajorID) REFERENCES Majors(MajorID)
)


CREATE TABLE Agenda (
	StudentID int,
	SubjectID int,

	CONSTRAINT PK_Agenda PRIMARY KEY(StudentID, SubjectID),
	CONSTRAINT FK_Agenda_Subjects FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID),
	CONSTRAINT FK_Agenda_Students FOREIGN KEY(StudentID) REFERENCES Students(StudentID)
)

CREATE TABLE Payments (
	PaymentID int PRIMARY KEY IDENTITY,
	PaymentDate date,
	PaymentAmount money,
	StudentID int NOT NULL,

	CONSTRAINT FK_Payments_Students FOREIGN KEY(StudentID) REFERENCES Students(StudentID)
)