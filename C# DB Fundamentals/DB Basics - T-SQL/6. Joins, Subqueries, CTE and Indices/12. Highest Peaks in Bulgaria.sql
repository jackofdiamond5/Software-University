SELECT 
	co.CountryCode,
	m.MountainRange,
	p.PeakName,
	p.Elevation
FROM Countries AS co
JOIN MountainsCountries AS mc ON mc.CountryCode = co.CountryCode
JOIN Mountains AS m ON m.Id = mc.MountainId
JOIN Peaks AS p ON p.MountainId = m.Id
WHERE co.CountryCode = 'BG' AND p.Elevation > 2835
ORDER BY p.Elevation DESC