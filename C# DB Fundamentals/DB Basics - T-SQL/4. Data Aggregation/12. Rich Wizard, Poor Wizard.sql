SELECT SUM(DepositDifference) AS SumDifference 
FROM
(
	SELECT 
		DepositAmount AS [First Wizard Deposit],
		LEAD(DepositAmount) OVER (ORDER BY Id) AS [Second Wizard Deposit],
		DepositAmount - LEAD(DepositAmount) OVER (ORDER BY Id) AS [DepositDifference]
	FROM WizzardDeposits
) AS SumDifference


-- Another solution

DECLARE @hostWizardAmount decimal(8,2);
DECLARE @guestWizardAmount decimal(8,2);
DECLARE @i int = 1;
DECLARE @sum decimal(8,2) = 0;
DECLARE @rows int = (SELECT COUNT(*) FROM WizzardDeposits);

WHILE @i < @rows
	BEGIN
		SET @hostWizardAmount = (
			SELECT 
				DepositAmount
			FROM (
				SELECT 
					DepositAmount,
					ROW_NUMBER() OVER(ORDER BY (SELECT 0)) AS RowNum
				FROM WizzardDeposits		
			) AS dr
			WHERE dr.RowNum = @i
		);

		SET @guestWizardAmount = (
			SELECT 
				DepositAmount
			FROM (
				SELECT 
					DepositAmount,
					ROW_NUMBER() OVER(ORDER BY (SELECT 0)) AS RowNum
				FROM WizzardDeposits		
			) AS dr
			WHERE dr.RowNum = @i + 1
		);

		SET @sum += @hostWizardAmount - @guestWizardAmount;
		SET @i += 1;
	END

SELECT @sum AS SumDifference