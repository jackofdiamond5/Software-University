using System;

public class Program
{
    public static void Main()
    {
        var bankAccount = new BankAccount
        {
            Id = 1,
        };
        
        bankAccount.Deposit(15);
        bankAccount.Withdraw(10);

        Console.WriteLine(bankAccount);
    }
}

