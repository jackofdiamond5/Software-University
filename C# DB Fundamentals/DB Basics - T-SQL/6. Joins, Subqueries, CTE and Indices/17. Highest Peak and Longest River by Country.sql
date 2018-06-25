SELECT TOP 5
	MC.CountryName,
	CR.MaxMountainHeight AS HighestPeakElevation,
	MC.MaxRiverLength AS LongestRiverLength
FROM (
	SELECT 
		cou.CountryName,
		MAX(r.Length) AS MaxRiverLength 
	FROM Countries AS cou
	LEFT JOIN CountriesRivers AS cr ON cr.CountryCode = cou.CountryCode
	LEFT JOIN Rivers AS r ON r.Id = cr.RiverId
	GROUP BY cou.CountryName
) AS MC
JOIN (
	SELECT
		cou.CountryName,
		MAX(p.Elevation) AS MaxMountainHeight
	FROM Countries AS cou
	LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = cou.CountryCode
	LEFT JOIN Mountains AS m ON m.Id = mc.MountainId
	LEFT JOIN Peaks AS p ON p.MountainId = m.Id
	GROUP BY cou.CountryName
) AS CR ON CR.CountryName = MC.CountryName
ORDER BY HighestPeakElevation DESC, LongestRiverLength DESC, CountryName