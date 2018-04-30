SELECT 
	SUBSTRING(FirstName, 1, 1) AS FirstLetter
FROM WizzardDeposits
WHERE DepositGroup = 'Troll Chest'
GROUP BY LEFT(FirstName, 1)
ORDER BY FirstLetter