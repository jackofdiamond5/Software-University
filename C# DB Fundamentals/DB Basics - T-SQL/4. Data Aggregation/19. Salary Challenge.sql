WITH CTE_AverageSalaryPerDepartment AS (
	SELECT 
		DepartmentId,
		AVG(Salary) AS AverageSalary
	FROM Employees
	GROUP BY DepartmentId
)

SELECT TOP 10
	emp.FirstName,
	emp.LastName,
	emp.DepartmentId
FROM Employees AS emp
JOIN CTE_AverageSalaryPerDepartment AS avgSalary ON emp.DepartmentID = avgSalary.DepartmentID
WHERE emp.Salary > avgSalary.AverageSalary
ORDER BY DepartmentId