SELECT 
	e.EmployeeID,
	e.Firstname,
	e.LastName,
	d.Name AS DepartmentName
FROM Employees AS e
JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
WHERE d.Name = 'Sales'
ORDER BY e.EmployeeID