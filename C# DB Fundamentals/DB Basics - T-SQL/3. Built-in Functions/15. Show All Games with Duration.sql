SELECT 
	Name AS Game,
	CASE 
		WHEN DATEPART(Hour, Start) BETWEEN 0 AND 11 THEN 'Morning'
		WHEN DATEPART(Hour, Start) BETWEEN 12 AND 17 THEN 'Afternoon'
		WHEN DATEPART(Hour, Start) BETWEEN 18 AND 24 THEN 'Evening'
	END AS [Part of the Day],
	CASE
		WHEN Duration IS NULL THEN 'Extra Long'
		WHEN CAST(Duration AS int) <= 3 THEN 'Extra Short'
		WHEN CAST(Duration AS int) BETWEEN 4 AND 6 THEN 'Short'
		WHEN CAST(Duration AS int) > 6 THEN 'Long'
	END AS Duration
FROM Games
ORDER BY Game, Duration, [Part of the Day]