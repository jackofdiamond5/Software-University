SELECT DISTINCT
	PeakName,
	RiverName,
	LOWER(STUFF(RiverName, 1, 1, PeakName)) AS [Mix]
FROM Peaks, Rivers
WHERE RIGHT(PeakName, 1) = LEFT(RiverName, 1)



-- Other solution
SELECT DISTINCT
	PeakName,
	RiverName,
	LOWER(CONCAT(PeakName, SUBSTRING(RiverName, 2, LEN(RiverName)))) AS [Mix]
FROM Peaks, Rivers
WHERE RIGHT(PeakName, 1) = LEFT(RiverName, 1)
