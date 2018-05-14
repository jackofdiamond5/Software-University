CREATE TABLE Teachers (
	TeacherID int PRIMARY KEY,
	Name nvarchar(50) NOT NULL,
	ManagerID int,

	CONSTRAINT FK_TeacherID_ManagerID FOREIGN KEY(ManagerID) REFERENCES Teachers(TeacherID)
)

INSERT INTO Teachers (TeacherID, Name, ManagerID) VALUES
(101, 'John', NULL),
(102, 'Maya', 106),
(103, 'Silvia', 106),
(104, 'Ted', 105),
(105, 'Mark', 101),
(106, 'Greta', 101)
