WITH CTE_CountriesAndCurrencies AS (
	SELECT 
		ContinentCode,
		CurrencyCode,
		COUNT(CountryCode) AS CurrencyUsage
	FROM Countries
	GROUP BY ContinentCode, CurrencyCode
	HAVING COUNT(CountryCode) > 1
)

SELECT
	cac.ContinentCode,
	cac.CurrencyCode,
	MaxCurUsage AS CurrencyUsage
FROM (
	SELECT
		ContinentCode,
		MAX(CurrencyUsage) AS MaxCurUsage
	FROM CTE_CountriesAndCurrencies
	GROUP BY ContinentCode
) AS mupc
JOIN CTE_CountriesAndCurrencies AS cac ON 
				cac.ContinentCode = mupc.ContinentCode AND 
				cac.CurrencyUsage = mupc.MaxCurUsage
ORDER BY cac.ContinentCode
