SELECT TOP 50 
	e.EmployeeId,
	CONCAT(FirstName, ' ', LastName) AS EmployeeName,
	m.ManagerName,
	d.Name AS DepartmentName
FROM Employees AS e
JOIN (
	SELECT 
		CONCAT(FirstName, ' ', LastName) AS ManagerName,
		EmployeeId
	FROM Employees
) AS m ON m.EmployeeID = e.ManagerId
JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
ORDER BY e.EmployeeID