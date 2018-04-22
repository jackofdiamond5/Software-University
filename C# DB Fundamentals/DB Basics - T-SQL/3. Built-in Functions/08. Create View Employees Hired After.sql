CREATE VIEW V_EmployeesHiredAfter2000
AS
SELECT FirstName, Lastname FROM Employees
WHERE HireDate > '2000/12/31'