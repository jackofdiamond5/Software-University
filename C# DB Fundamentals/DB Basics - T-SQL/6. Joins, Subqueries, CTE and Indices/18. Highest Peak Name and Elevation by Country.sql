SELECT TOP 5 WITH TIES 
	Country,
	ISNULL(p.PeakName, '(no highest peak)') AS [Highest Peak Name],
	ISNULL(MaxElevation, 0) AS [Highest Peak Elevation],
	ISNULL(Mountain, '(no mountain)') AS Mountain
FROM (
	SELECT
		cou.CountryName AS Country,
		MAX(p.Elevation) AS MaxElevation,
		m.MountainRange AS Mountain,
		m.Id AS MountainId
	FROM Countries AS cou
	LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = cou.CountryCode
	LEFT JOIN Mountains AS m ON m.Id = mc.MountainId
	LEFT JOIN Peaks AS p ON p.MountainId = m.Id 
	GROUP BY cou.CountryName, m.MountainRange, m.Id
) AS mc
LEFT JOIN Peaks AS p ON p.MountainId = mc.MountainId
ORDER BY Country, [Highest Peak Name]