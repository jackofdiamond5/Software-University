SELECT TOP 5
	cou.CountryName,
	r.RiverName
FROM Countries AS cou
JOIN Continents AS co ON co.ContinentCode = cou.ContinentCode
LEFT JOIN CountriesRivers AS cr ON cr.CountryCode = cou.CountryCode
LEFT JOIN Rivers AS r ON r.Id = cr.RiverId
WHERE co.ContinentName = 'Africa'
ORDER BY cou.CountryName
