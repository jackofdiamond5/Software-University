SELECT 
	DepartmentID,
	Salary AS ThirdHighestSalary
FROM
(
	SELECT 
		DepartmentID,
		Salary,
		DENSE_RANK() OVER (PARTITION BY DepartmentId ORDER BY Salary DESC) AS SalaryRank
	FROM Employees
) AS Salaries
WHERE Salaries.SalaryRank = 3
GROUP BY DepartmentID, Salary

-- Another solution 

WITH CTE_Numbered AS (
	SELECT DISTINCT
		DepartmentId,
		Salary,
		DENSE_RANK() OVER(PARTITION BY DepartmentId ORDER BY Salary DESC) AS RowNum
	FROM Employees
)

SELECT  
	DepartmentId,
	Salary
FROM CTE_Numbered
WHERE RowNum = 3 