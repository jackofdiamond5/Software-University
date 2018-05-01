SELECT SUM(DepositDifference) AS SumDifference 
FROM
(
	SELECT 
		DepositAmount AS [First Wizard Deposit],
		LEAD(DepositAmount) OVER (ORDER BY Id) AS [Second Wizard Deposit],
		DepositAmount - LEAD(DepositAmount) OVER (ORDER BY Id) AS [DepositDifference]
	FROM WizzardDeposits
) AS SumDifference