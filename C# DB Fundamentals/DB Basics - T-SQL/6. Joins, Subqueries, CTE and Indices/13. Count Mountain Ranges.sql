SELECT 
	co.CountryCode,
	mr.MountainRanges
FROM Countries AS co
JOIN (
	SELECT
		co.CountryCode, 
		COUNT(*) AS MountainRanges
	FROM Mountains AS m
	JOIN MountainsCountries AS mc ON mc.MountainId = m.Id
	JOIN Countries AS co ON co.CountryCode = mc.CountryCode
	GROUP BY co.CountryCode
) AS mr ON mr.CountryCode = co.CountryCode
WHERE co.CountryCode IN('US', 'RU', 'BG')