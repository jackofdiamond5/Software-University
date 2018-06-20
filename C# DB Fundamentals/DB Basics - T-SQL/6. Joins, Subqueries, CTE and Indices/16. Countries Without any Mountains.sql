SELECT
	COUNT(cou.CountryCode) AS CountryCode
FROM Countries AS cou
FULL JOIN MountainsCountries AS mc ON mc.CountryCode = cou.CountryCode
FULL JOIN Mountains AS m ON m.Id = mc.MountainId
WHERE mc.MountainId IS NULL